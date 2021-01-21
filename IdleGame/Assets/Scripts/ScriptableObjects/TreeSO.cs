using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Tree", menuName = "Custom Data/Tree")]
    public class TreeSO : ScriptableObject
    {
        public string Name;
        public float Speed;
        public int UnlockLevel;
        public int ExpAmount;
    }
}
