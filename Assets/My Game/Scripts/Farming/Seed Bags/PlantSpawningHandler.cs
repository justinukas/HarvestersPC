using System.Collections.Generic;
using UnityEngine;

namespace Main.Farming
{
    public class PlantSpawningHandler : MonoBehaviour
    {
        PlantInfoHandler PlantInfoHandler;

        private void Start()
        {
            PlantInfoHandler = GetComponent<PlantInfoHandler>();
        }

        // Instantiate plants at fixed locations on the top surface of the dirt
        public void SpawnPlants(string bagVariant, GameObject tilledDirt)
        {
            if (PlantInfoHandler.plantInfo.TryGetValue(bagVariant, out var info))
            {
                List<Vector3> positions = info.Item1;
                Transform parent = info.Item2;
                GameObject plant = info.Item3;

                foreach (Vector3 spawnOffset in positions)
                {
                    GameObject plantClone = Instantiate(plant, spawnOffset + tilledDirt.transform.position, Quaternion.identity);
                    plantClone.transform.SetParent(parent);
                    plantClone.name = plant.name;
                }
            }
        }
    }
}
