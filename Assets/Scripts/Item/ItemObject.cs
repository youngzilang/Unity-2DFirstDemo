using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData item;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 vector;

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
        if (!Inventory.instance.BagFullOrNot() && item.itemType == ItemType.Equipment)
        {
            rb.velocity = new Vector2(0, 7);
            //调用飘字
            PlayerManager.instance.player.fX.CreatePopUpText("背包已满");
            return;
        }
        AudioManager.instance.PlaySFX(12,transform);
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
