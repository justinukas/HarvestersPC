using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemPositionAndRotation : MonoBehaviour
    {
        Vector3 offsetItem;

        [SerializeField] private Transform defaultToolPosition;

        public void UpdateItemPositionAndRotation(string currentItem, GameObject grabbedTool)
        {
            if (currentItem == "null" || grabbedTool == null) return;

            // set offsets & rotations
            switch (currentItem)
            {
                case "Scythe":
                    offsetItem = new Vector3(0, 0, 0);
                    grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(-90, 180, 90); break;

                case "Axe":
                    offsetItem = new Vector3(0, 0, 0);
                    grabbedTool.transform.rotation = defaultToolPosition.rotation; break;

                case "Hoe":
                    offsetItem = new Vector3(0, 0, 0);
                    grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, -90, 0); break;

                case "Bag":
                    offsetItem = new Vector3(0, 0, 0);
                    grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, 0, 0); break;

                case "Seed Bag":
                    offsetItem = new Vector3(0, 0, 0);
                    grabbedTool.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, 180, 0); break;
            }
            grabbedTool.transform.position = defaultToolPosition.position + offsetItem;
        }
    }
}
