using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour,ISaveManager
{
    public static Inventory instance;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    public List<InventoryItem> equipment;
    public Dictionary<ItemDataEquipment, InventoryItem> equipmentDictionary;

    public List<ItemData> justEquipments;

   [Header("Inventory UI")]
    [SerializeField]private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipSlotParent;
    [SerializeField] private Transform statSlotParent;

    public ItemSlotUI[] inventoryItemSlot;
    public ItemSlotUI[] stashItemSlot;
    public EquipmentUI[] equipItemSlot;
    public StatSlotUI[] statSlot;

    private float lastFlaskTime;
    private float lastDieArmorTime;

    public float flaskTime { get; private set; }
    private float dieArmorTime;

    [Header("存档数据")]
    public List<InventoryItem> loadItems;
    public List<ItemDataEquipment> loadEquipments;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<ItemSlotUI>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<ItemSlotUI>();
        equipItemSlot = equipSlotParent.GetComponentsInChildren<EquipmentUI>();
        statSlot = statSlotParent.GetComponentsInChildren<StatSlotUI>();

        JustEquipmentsAdd();
    }

    private void JustEquipmentsAdd()
    {
        foreach(var equipment in loadEquipments)
        {
            Equip(equipment);
        }

        if (loadItems.Count > 0)
        {
            foreach(InventoryItem inventoryItem in loadItems)
            {
                for(int i=0;i< inventoryItem.stackSize; i++)
                {
                    AddItem(inventoryItem.data);
                }
            }
            return;
        }


        for (int i = 0; i < justEquipments.Count; i++)
        {
            AddItem(justEquipments[i]);
        }
    }

    public void Equip(ItemData _item)
    {
        ItemDataEquipment newEquipment = _item as ItemDataEquipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemDataEquipment OldEquipment = null;

        foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType) OldEquipment = item.Key;
        }


        if (OldEquipment != null)
        {
            UnEquip(OldEquipment);
            AddItem(OldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModify();
        RemoveItem(_item);
    }

    public void UnEquip(ItemDataEquipment OldEquipment)
    {
        if (equipmentDictionary.TryGetValue(OldEquipment, out InventoryItem item1))
        {
            equipment.Remove(item1);
            equipmentDictionary.Remove(OldEquipment);
            OldEquipment.RemoveModify();
        }
    }

    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment&&BagFullOrNot()) AddToInventory(_item);
        else if (_item.itemType == ItemType.Material) AddToStash(_item);

            UpdateSlotUI();
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }
    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else value.RemoveStack();
        }

        if (stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else stashValue.RemoveStack();
        }

        UpdateSlotUI();
    }

    public void  UpdateSlotUI()
    {
        for (int i = 0; i < equipItemSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipItemSlot[i].slotType) equipItemSlot[i].UpdateSlotUI(item.Value);
            }
        }



        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }
        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlotUI(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlotUI(stash[i]);
        }

        UpdateStatUI();
    }

    public void UpdateStatUI()
    {
        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValue();
        }
    }

    public bool CraftOrNot(ItemDataEquipment equipmentToCraft,List<InventoryItem> requiredMateril)
    {
        List<InventoryItem> toUsedMaterial =new List<InventoryItem>();

        for(int i = 0; i < requiredMateril.Count; i++)
        {

            if (stashDictionary.TryGetValue(requiredMateril[i].data,out InventoryItem value))
            {
                if (value.stackSize < requiredMateril[i].stackSize)
                {
                    Debug.Log("材料不足！");
                    return false;
                }
                else
                {
                    toUsedMaterial.Add(value);
                }
            }
            else
            {
                Debug.Log("材料不足！");
                return false;
            }
        }

        for(int i = 0; i < requiredMateril.Count; i++)
        {
            for (int j = 0; j < requiredMateril[i].stackSize; j++) RemoveItem(requiredMateril[i].data);
        }

        AddItem(equipmentToCraft);

        return true;

    }


    public List<InventoryItem> GetEquipments() => equipment;

    public ItemDataEquipment GetEquipmentByType(EquipmentType _type)
    {
        ItemDataEquipment toGetEquipment = null;

        foreach (KeyValuePair<ItemDataEquipment, InventoryItem> equip in equipmentDictionary)
        {
            if (equip.Key.equipmentType == _type)
            {
                toGetEquipment = equip.Key;
                break;
            }
        }
        return toGetEquipment;
    }

    public void UseFlask()
    {
        ItemDataEquipment equipment = GetEquipmentByType(EquipmentType.Flask);

        if (!equipment) return;

        if (Time.time > flaskTime + lastFlaskTime)
        {
            flaskTime = equipment.flaskCd;
            equipment.UseItemEffect(null);
            lastFlaskTime = Time.time;
        }
        else Debug.Log("冷却中!");
    }

    public bool UseArmor()
    {
        ItemDataEquipment equipment = GetEquipmentByType(EquipmentType.Armor);

        if (!equipment) return false;

        if (Time.time > dieArmorTime + lastDieArmorTime)
        {
            dieArmorTime = equipment.armorCd;
            lastDieArmorTime = Time.time;
            return true;
        }
        else Debug.Log("冷却中!");
        return false;
    }

    public bool BagFullOrNot()
    {
        if (inventory.Count >= inventoryItemSlot.Length)
        {
            Debug.Log("背包已满!");
            return false;
        }
        return true;
    }

    public void LoadData(GameData _data)
    {
       foreach(KeyValuePair<string,int> keyValuePair in _data.inventory)
        {
            foreach(var item in GetItemDataBase())
            {
                if(item!= null && item.itemId == keyValuePair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = keyValuePair.Value;

                    loadItems.Add(itemToLoad);
                }
            }
        }

       foreach(string id in _data.equipmentIds)
        {
            foreach(var equip in GetItemDataBase())
            {
                if (equip != null && equip.itemId == id)
                {
                    loadEquipments.Add(equip as ItemDataEquipment);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipmentIds.Clear();

        foreach(KeyValuePair<ItemData,InventoryItem> keyValuePair in inventoryDictionary)
        {
            _data.inventory.Add(keyValuePair.Key.itemId, keyValuePair.Value.stackSize);
        }

        foreach(KeyValuePair<ItemData, InventoryItem> keyValuePair in stashDictionary)
        {
            _data.inventory.Add(keyValuePair.Key.itemId, keyValuePair.Value.stackSize);
        }

        foreach(KeyValuePair<ItemDataEquipment, InventoryItem> keyValuePair in equipmentDictionary)
        {
            _data.equipmentIds.Add(keyValuePair.Key.itemId);
        }
    }

    public List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetName = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" });

        foreach(string SOName in assetName)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }
        return itemDataBase;
    }

}
