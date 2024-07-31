using UnityEngine;

namespace Main
{
    public static class ItemNames
    {
        public static string[] itemNames = new string[] { "Scythe", "Axe", "Hoe", "Bag", "Carrot Seed Bag", "Wheat Seed Bag" };
    }


    public class KeepOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
