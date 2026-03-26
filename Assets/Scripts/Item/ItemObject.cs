using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
   

    [SerializeField] private ItemData item;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;
        gameObject.name ="Item - "+ item.name;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Debug.Log("Pick up " + item.itemName);
            Inventory.instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
