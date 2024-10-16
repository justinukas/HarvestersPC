using UnityEngine;

namespace Main.ItemHandling
{
    public static class ItemNames
    {
        public static string[] itemNames = new string[] { "Scythe", "Axe", "Hoe", "Bag", "Carrot Seed Bag", "Wheat Seed Bag" /* . . . */ };
        public static string[] plantNames = new string[] { "Carrot" /* . . . */ };
    }


    public class KeepOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
