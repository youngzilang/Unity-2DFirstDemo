using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private ItemData item; 

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.icon;
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
