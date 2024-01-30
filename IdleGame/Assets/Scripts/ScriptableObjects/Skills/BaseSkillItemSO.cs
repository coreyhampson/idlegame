using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class BaseSkillItemSO : ScriptableObject
    {
        public Sprite itemSprite;
        public string Name;
        public string Description;
        public float Speed;
        public int UnlockLevel;
        public int ExpAmount;
        public float BaseSellValue;
    }
}
