using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData item;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 vector;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) rb.velocity = vector;
    }

    public void SetUpItemObject(ItemData _itemData,Vector2 _vector)
    {
        item = _itemData;
        rb.velocity = _vector;

        if (item == null) return;

        GetComponent<SpriteRenderer>().sprite = item.icon;
        gameObject.name = "Item - " + item.name;
    }

    public void PickUpItem()
    {
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
