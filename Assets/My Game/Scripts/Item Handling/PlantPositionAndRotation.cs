using UnityEngine;

namespace Main.ItemHandling
{
    public class PlantPositionAndRotation : MonoBehaviour
    {
        Vector3 offsetPlant;
        [SerializeField] private Transform defaultPlantPosition;
        public void UpdatePlantPositionAndRotation(string currentPlant, GameObject grabbedPlant)
        {
            if (currentPlant == "null" || grabbedPlant == null) return;

            switch (currentPlant)
            {
                case "Carrot":
                    offsetPlant = new Vector3(0, 0, 0);
                    grabbedPlant.transform.rotation = defaultPlantPosition.rotation; break;
            }
            grabbedPlant.transform.position = defaultPlantPosition.position + offsetPlant;
        }
    }
}
