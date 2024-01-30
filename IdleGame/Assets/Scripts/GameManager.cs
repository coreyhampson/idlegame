using Assets.Scripts.ScriptableObjects;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SerializedDictionary<BaseSkillItemSO, int> inventoryItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void AddItem(BaseSkillItemSO itemToAdd, int itemAmount)
    {
        if (inventoryItems.ContainsKey(itemToAdd))
        {
            inventoryItems[itemToAdd] += itemAmount;
        }
        else
        {
            inventoryItems.Add(itemToAdd, itemAmount);
        }

        UIMenu.instance.ShowItems();
    }

    //public void RemoveItem(string itemToRemove, int itemAmount)
    //{
    //    bool foundItem = false;
    //    int itemPosition = 0;

    //    for(int i = 0; i < itemsHeld.Length; i++)
    //    {
    //        if(itemsHeld[i] == itemToRemove)
    //        {
    //            foundItem = true;
    //            itemPosition = i;
    //            i = itemsHeld.Length;
    //        }
    //    }

    //    if(foundItem)
    //    {
    //        numberOfItems[itemPosition] -= itemAmount;

    //        for(int i = 0; i < referenceItems.Length; i++)
    //        {
    //            if(referenceItems[i].itemName == itemToRemove)
    //            {
    //                referenceItems[i].itemAmount -= itemAmount;
    //            }
    //        }

    //        if(numberOfItems[itemPosition] <= 0)
    //        {
    //            itemsHeld[itemPosition] = "";
    //        }

    //        UIMenu.instance.ShowItems();
    //    }
    //    else
    //    {
    //        Debug.LogError("Item name does not exist in inventory!");
    //    }
    //}
}
