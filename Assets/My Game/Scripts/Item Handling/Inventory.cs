using UnityEngine;
using System.Collections.Generic;

namespace Main.ItemHandling
{
    public class Inventory : MonoBehaviour
    {
        public class Items
        {
            public string name;
            public GameObject sprite;
            public GameObject gameobject;
        }

        private Dictionary<int, (string, GameObject, GameObject)> invSlots;

        private ItemManager itemManager;

        private void Update()
        {
            EquipItem();
        }

        private void ExtendUI()
        {

        }

        private void EquipItem()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

            }

            if (Input.GetKeyDown(KeyCode.Alpha2)) 
            {
                
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {

            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {

            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {

            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {

            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {

            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {

            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {

            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {

            }
        }
    }
}
