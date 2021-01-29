using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Custom Data/Items/New Item")]
public class ItemSO : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Sprite;
    public ItemType Type;
    public string Description;
    public float Value;

    public enum ItemType
    {
        Item,
        Weapon,
        Armour
    }
}
