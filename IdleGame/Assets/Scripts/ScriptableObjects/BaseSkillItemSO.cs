using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class BaseSkillItemSO : ScriptableObject
    {
        public string Name;
        public float Speed;
        public int UnlockLevel;
        public int ExpAmount;
    }
}
