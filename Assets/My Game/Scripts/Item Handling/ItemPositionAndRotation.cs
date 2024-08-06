using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemPositionAndRotation : MonoBehaviour
    {
        public void UpdateItemPositionAndRotation(string currentTool, GameObject grabbedTool, string currentPlant, GameObject grabbedPlant, Transform defaultToolPosition, Transform defaultPlantPosition)
        {
            // set rotations
            if (grabbedTool != null)
            {
                switch (currentTool)
                {
                    case "Scythe":
                        grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(-90, 180, 90); break;

                    case "Axe":
                        grabbedTool.transform.rotation = defaultToolPosition.rotation; break;

                    case "Hoe":
                        grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, -90, 0); break;

                    case "Bag":
                        grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, 0, 0); break;

                    case "Seed Bag":
                        grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, 180, 0); break;
                }
                grabbedTool.transform.parent = defaultToolPosition;
                grabbedTool.transform.position = defaultToolPosition.position;
            }
           
            if (grabbedPlant != null)
            {
                switch (currentPlant)
                {
                    case "Carrot":
                        grabbedPlant.transform.rotation = defaultPlantPosition.rotation; break;
                }
                grabbedPlant.transform.position = defaultPlantPosition.position;
                grabbedPlant.transform.parent = defaultPlantPosition;
            }
        }
    }
}
