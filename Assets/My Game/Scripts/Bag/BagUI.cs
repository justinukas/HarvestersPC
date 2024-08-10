using System.Collections.Generic;
using UnityEngine;

namespace Main.Bag
{
    public class BagUI : MonoBehaviour
    {
        [System.Serializable]
        public class PlantUI
        {
            public string plantName;
            public GameObject plantUIElement;
        }

        public List<PlantUI> plantUIList;
        public GameObject InventoryUI;

        private Dictionary<string, GameObject> plantUIDictionary;
        public int plantUICount;

        private void Start()
        {
            plantUIDictionary = new Dictionary<string, GameObject>();
            foreach (var plantUI in plantUIList)
            {
                plantUIDictionary[plantUI.plantName] = plantUI.plantUIElement;
            }
        }

        public void SpawnUI(string plant)
        {
            if (plantUIDictionary.TryGetValue(plant, out GameObject plantCounter))
            {
                if (!plantCounter.activeInHierarchy)
                {
                    if (!InventoryUI.activeInHierarchy)
                    {
                        InventoryUI.SetActive(true);
                        InventoryUI.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
                    }

                    plantCounter.SetActive(true);
                    plantCounter.GetComponent<RectTransform>().anchoredPosition = new Vector2(plantUICount * 120, plantCounter.GetComponent<RectTransform>().anchoredPosition.y);
                    InventoryUI.GetComponent<RectTransform>().sizeDelta = new Vector2(160 + 120 * plantUICount, 160);
                    plantUICount++;
                }
            }
        }

        public void DisableAllUIElements()
        {
            foreach (GameObject uiElement in plantUIDictionary.Values)
            {
                uiElement.SetActive(false);
            }
            InventoryUI.SetActive(false);
            plantUICount = 0;
        }
    }
}
