using Assets.Scripts.ScriptableObjects;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public BaseSkillItemSO key;
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;

    private void Start()
    {
        amountText.text = "";
    }

    public void Press()
    {
        if (GameManager.instance.inventoryItems.Any())
        {
            UIMenu.instance.SelectItem(key);
        }
        else
        {
            UIMenu.instance.itemName.text = "Item Name";
            UIMenu.instance.itemDescription.text = "Item Description";
            UIMenu.instance.itemValue.text = "Item Value";
            UIMenu.instance.activeItem = null;
        }
    }
}
