using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Farming
{

    // CHATGPT COMING IN CLUTCH WITH THIS. LET IT COOK NOW
    public class PlantGrowth : MonoBehaviour
    {
        [HideInInspector] public float growthRate = 0.01f;

        private Dictionary<string, (float MaxHeight, Color? GrownColor, string ChildName)> plantTypes;
        private void Start()
        {
            InitializePlantTypes();
            StartCoroutine(GrowPlants());
        }

        private void InitializePlantTypes()
        {
            plantTypes = new Dictionary<string, (float MaxHeight, Color? GrownColor, string ChildName)>
            {
                {"WheatParent", (0.629f, new Color(0.9960784f, 0.9490196f, 0.5707546f), "Wheat(Clone)") },
                {"CarrotParent", (0.10f, null, "Carrot(Clone)") }
                // add new plants here
            };
        }

        private IEnumerator GrowPlants()
        {
            while (true)
            {
                foreach (var plantType in plantTypes)
                {
                    foreach (GameObject plantParent in GameObject.FindGameObjectsWithTag(plantType.Key))
                    {
                        HandlePlantGrowth(plantParent, plantType.Value);
                    }
                }
                yield return new WaitForSeconds(1.5f);
            }
        }

        private void HandlePlantGrowth(GameObject plantParent, (float MaxHeight, Color? GrownColor, string ChildName) plantType)
        {
            if (plantParent.transform.position.y < plantType.MaxHeight && plantParent.transform.childCount > 0)
            {
                plantParent.transform.position = new Vector3(plantParent.transform.position.x, plantParent.transform.position.y + Time.deltaTime * growthRate, plantParent.transform.position.z);
            }
            else if (plantParent.transform.position.y >= plantType.MaxHeight)
            {
                plantParent.transform.position = new Vector3(plantParent.transform.position.x, plantType.MaxHeight, plantParent.transform.position.z);
            }

            if (plantParent.transform.position.y >= plantType.MaxHeight && plantParent.transform.childCount > 0)
            {
                foreach (Transform child in plantParent.transform)
                {
                    child.GetComponent<Harvestability>().isHarvestable = true;
                    if (plantType.GrownColor != null)
                    {
                        child.GetComponent<Renderer>().material.color = plantType.GrownColor.Value;
                    }
                }
            }
            if (plantParent.transform.childCount == 0) plantParent.transform.position = Vector3.zero;
        }
    }
}
