using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftListUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform craftSlotParent;
    [SerializeField] private GameObject craftSlotPrefab;

    [SerializeField] private List<ItemDataEquipment> equipment;


    private void Start()
    {
        transform.parent.GetChild(0).GetComponent<CraftListUI>().SetUpCraftList();
        SetUpDefaultWindow();
    }

    public void SetUpCraftList()
    {
        for (int i = 0; i < craftSlotParent.childCount; i++)
        {
            Destroy(craftSlotParent.GetChild(i).gameObject);
        }



        for (int i = 0; i < equipment.Count; i++)
        {
            GameObject newSlot = Instantiate(craftSlotPrefab, craftSlotParent);
            newSlot.GetComponent<CraftSlotUI>().SetUpCraftSlot(equipment[i]);
        }
    }

    public void SetUpDefaultWindow()
    {
        if (equipment == null || equipment.Count == 0)
        {
            Debug.LogWarning("该装备暂无配方!!!");
            return;
        }
        GetComponentInParent<UI>().craftWindow.SetUpCraftWindow(equipment[0]);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetUpCraftList();
    }
}
