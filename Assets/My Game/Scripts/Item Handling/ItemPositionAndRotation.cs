using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemPositionAndRotation : MonoBehaviour
    {
        //Vector3 offsetItem = Vector3.zero;
        //Vector3 offsetPlant = Vector3.zero;

        public void UpdateItemPositionAndRotation(string currentItem, GameObject grabbedTool, string currentPlant, GameObject grabbedPlant, Transform defaultToolPosition, Transform defaultPlantPosition)
        {
            // set offsets & rotations
            if (grabbedTool != null)
            {
                switch (currentItem)
                {
                    case "Scythe":
                        //offsetItem = new Vector3(0, 0, 0);
                        grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(-90, 180, 90); break;

                    case "Axe":
                        //offsetItem = new Vector3(0, 0, 0);
                        grabbedTool.transform.rotation = defaultToolPosition.rotation; break;

                    case "Hoe":
                        //offsetItem = new Vector3(0, 0, 0);
                        grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, -90, 0); break;

                    case "Bag":
                        //offsetItem = new Vector3(0, 0, 0);
                        grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, 0, 0); break;

                    case "Seed Bag":
                        //offsetItem = new Vector3(0, 0, 0);
                        grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, 180, 0); break;
                }
                grabbedTool.transform.position = defaultToolPosition.position /*+ offsetItem*/;
            }
           
            if (grabbedPlant != null)
            {
                switch (currentPlant)
                {
                    case "Carrot":
                        //offsetPlant = new Vector3(0, 0, 0);
                        grabbedPlant.transform.rotation = defaultPlantPosition.rotation; break;
                }
                grabbedPlant.transform.position = defaultPlantPosition.position /*+ offsetPlant*/;
            }
        }
    }
}
