using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private ItemData[] possibleDrop;
    [SerializeField]private int dropAmount;
    private List<ItemData> DropList = new List<ItemData>();

    public virtual void GenerateDropObject()
    {
        for(int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) < possibleDrop[i].dropChance) DropList.Add(possibleDrop[i]);
        }

        if (DropList.Count <= 0) return;

        int actualDrop = Mathf.Min(dropAmount, DropList.Count);

        for (int i = 0; i < actualDrop; i++)
        {
            ItemData itemData = DropList[Random.Range(0, DropList.Count)];
            DropList.Remove(itemData);
            DropItemObject(itemData);
        }
    }


    public void DropItemObject(ItemData _itemData)
    {
        GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);

        Vector2 vector = new Vector2(Random.Range(-5, 5), Random.Range(15,20)); 

        newObject.GetComponent<ItemObject>().SetUpItemObject(_itemData,vector);
    }
}
