using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftWindowUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button craftButton;
    [SerializeField] private Image[] meterialImage;

    public void SetUpCraftWindow(ItemDataEquipment _equipment)
    {
        craftButton.onClick.RemoveAllListeners();

        for(int i = 0; i < meterialImage.Length; i++)
        {
            meterialImage[i].color = Color.clear;
            meterialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }

        for(int i = 0; i < _equipment.craftMaterial.Count; i++)
        {
            if (_equipment.craftMaterial.Count > meterialImage.Length) Debug.LogWarning("该装备制作所需材料种类超过4种，请控制在4种内!");

            meterialImage[i].sprite = _equipment.craftMaterial[i].data.icon;
            meterialImage[i].color = Color.white;

            TextMeshProUGUI meterialText = meterialImage[i].GetComponentInChildren<TextMeshProUGUI>();
            meterialText.text = _equipment.craftMaterial[i].stackSize.ToString();
            meterialText.color = Color.white;
        }

        itemName.text = _equipment.itemName;
        itemDescription.text = _equipment.Description();

        // 关键代码：文字太多时自动缩小字体
        itemDescription.enableAutoSizing = true;  // 开启自动缩放
        itemDescription.fontSizeMin = 14;         // 最小缩到 14 号
        itemDescription.fontSizeMax = 24;

        itemIcon.sprite = _equipment.icon;

        craftButton.onClick.AddListener(() => Inventory.instance.CraftOrNot(_equipment, _equipment.craftMaterial));
    }
}
