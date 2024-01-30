using Assets.Scripts.ScriptableObjects;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    public static UIMenu instance;
    public GameObject mainMenu;
    public GameObject[] menuButtons;
    public GameObject[] windows;
    public ItemButton[] inventorySlots;
    public BaseSkillItemSO activeItem;
    public string selectedItem;
    public Text itemName, itemDescription, itemValue, coinAmount;
    public Button[] itemUsageButtons;
    public Text inputHeader, messageBox;
    public GameObject inputPanel;
    public InputField inputBox;
    public Image inputItemImage;
    public Button inputNegative, inputPositive;
    public bool useInputImage;
    private bool dialogOpen;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
            menuButtons[i].GetComponentInChildren<Text>().color = Color.white;
            windows[2].SetActive(true);
            menuButtons[2].GetComponentInChildren<Text>().color = Color.red;
            mainMenu.SetActive(true);

        }
        inputPanel.SetActive(false);
        dialogOpen = false;
        coinAmount.text = "Gold Coins:\n" + PlayerStats.instance.money.ToString();
        itemUsageButtons[0].interactable = false;

        ToggleWindow(3);
    }

    public void ToggleWindow(int windowNumber)
    {
        if (!dialogOpen)
        {
            for (int i = 0; i < windows.Length; i++)
            {
                if (windows[i].gameObject.activeInHierarchy)
                {
                    windows[i].SetActive(false);
                    menuButtons[i].GetComponentInChildren<Text>().color = Color.white;
                }
                if (!windows[windowNumber].gameObject.activeInHierarchy)
                {
                    windows[windowNumber].SetActive(true);
                    menuButtons[windowNumber].GetComponentInChildren<Text>().color = Color.red;
                }
            }
        }
    }

    public void ShowItems()
    {
        foreach (var inventoryItem in GameManager.instance.inventoryItems)
        {
            var existingInventorySlot = inventorySlots.FirstOrDefault(x => x.key == inventoryItem.Key);

            if (inventoryItem.Key != null && existingInventorySlot != null)
            {
                existingInventorySlot.amountText.text = inventoryItem.Value.ToString();
            }
            else
            {
                var nextInventorySlot = inventorySlots.FirstOrDefault(x => x.key == null);

                nextInventorySlot.buttonImage.gameObject.SetActive(true);
                nextInventorySlot.buttonImage.sprite = inventoryItem.Key.itemSprite;
                nextInventorySlot.amountText.text = inventoryItem.Value.ToString();
                nextInventorySlot.key = inventoryItem.Key;
            }
        }
    }

    public void ClearSelected()
    {
        foreach (var slot in inventorySlots.Where(x => x.GetComponent<Image>().color == Color.white))
        {
            slot.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
        }
    }

    public void SelectItem(BaseSkillItemSO newItem)
    {
        ClearSelected();
        activeItem = newItem;
        
        itemName.text = activeItem.Name;
        itemDescription.text = activeItem.Description;
        itemValue.text = "Sell Value: " + activeItem.BaseSellValue.ToString() + " Gold Coins";
    }

    //public void SellItem()
    //{
    //    if (activeItem != null)
    //    {
    //        if (activeItem.itemAmount > 1)
    //        {
    //            useInputImage = true;
    //            inputItemImage.sprite = activeItem.itemSprite;
    //            inputNegative.onClick.AddListener(CloseInputBox);
    //            inputPositive.onClick.AddListener(ConfirmSellQuantity);
    //            ShowInputBox("Confirm Item Sell Quantity",
    //                   "How many " + activeItem.itemName
    //                   + " item(s) do you wish to sell?\n" +
    //                   "You own " + activeItem.itemAmount + " " + activeItem.itemName + " item(s).");
    //            return;
    //        }

    //        PlayerStats.instance.money += activeItem.itemValue;
    //        //GameManager.instance.RemoveItem(activeItem.itemName, 1);
    //        PlayerStats.instance.SaveStats();
    //        coinAmount.text = "Gold Coins:\n" + PlayerStats.instance.money.ToString();

    //        if (activeItem.itemAmount < 1)
    //        {
    //            activeItem = null;
    //            itemName.text = "Item Name";
    //            itemDescription.text = "Item Description";
    //            itemValue.text = "Item Value";
    //        }
    //    }
    //}

    public void ShowInputBox(string title, string message)
    {
        if (useInputImage)
        {
            inputItemImage.gameObject.SetActive(true);
        }
        else
        {
            inputItemImage.gameObject.SetActive(false);
        }
        inputHeader.text = title;
        messageBox.text = message;
        inputPanel.SetActive(true);
        dialogOpen = true;
    }

    public void CloseInputBox()
    {
        inputPanel.SetActive(false);
        dialogOpen = false;
        inputBox.text = "";
    }

    //public void ConfirmSellQuantity()
    //{
    //    if (inputBox != null)
    //    {
    //        int input;
    //        if (int.TryParse(inputBox.text, out input))
    //        {
    //            if (input > 0 && input < 99999)
    //            {
    //                if (input <= activeItem.itemAmount)
    //                {
    //                    PlayerStats.instance.money += activeItem.itemValue * input;
    //                    //GameManager.instance.RemoveItem(activeItem.itemName, input);
    //                    PlayerStats.instance.SaveStats();
    //                    coinAmount.text = "Gold Coins:\n" + PlayerStats.instance.money.ToString();
    //                    CloseInputBox();

    //                    if (activeItem.itemAmount < 1)
    //                    {
    //                        activeItem = null;
    //                        itemName.text = "Item Name";
    //                        itemDescription.text = "Item Description";
    //                        itemValue.text = "Item Value";
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.LogError("You don't have that many!");
    //                }
    //            }
    //            else
    //            {
    //                Debug.LogError("You entered wrong input usage. 1-99999");
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError("Text to int parsing failed.");
    //        }
    //    }
    //}

    //public void UseItem()
    //{
    //    if (activeItem != null)
    //    {
    //        if (activeItem.itemName == "Health Potion")
    //        {
    //            //GameManager.instance.RemoveItem(activeItem.itemName, 1);
    //            PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth;
    //            Debug.Log("You use a Health Potion and heal 100%.");

    //            Skilling.instance.playerHealth.maxValue = PlayerStats.instance.maxHealth; // Player Health
    //            Skilling.instance.playerHealth.value = PlayerStats.instance.currentHealth;

    //        }
    //        if (activeItem.itemAmount < 1)
    //        {
    //            activeItem = null;
    //            itemName.text = "Item Name";
    //            itemDescription.text = "Item Description";
    //            itemValue.text = "Item Value";
    //        }
    //        ShowItems();
    //        PlayerStats.instance.SaveStats();
    //    }
    //    else
    //    {
    //        Debug.LogError("No item active.");
    //    }
    //}
}
