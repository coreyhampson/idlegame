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
        ClearAllText();
    }

    void ClearAllText()
    {
        amountText.text = "";

        UIMenu.instance.itemName.text = "";
        UIMenu.instance.itemDescription.text = "";
        UIMenu.instance.itemValue.text = "";
        UIMenu.instance.activeItem = null;
    }

    public void Press()
    {
        if (key != null)
        {
            UIMenu.instance.SelectItem(key);
        }
        else
        {
            ClearAllText();
        }

        UIMenu.instance.ClearSelected();
        GetComponent<Image>().color = Color.white;
    }
}
