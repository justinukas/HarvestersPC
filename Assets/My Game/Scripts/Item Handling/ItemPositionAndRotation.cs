using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemPositionAndRotation : MonoBehaviour
    {
        public Transform defaultToolPosition;
        public void UpdateItemPositionAndRotation(string currentItem, GameObject grabbedObject)
        {
            if (currentItem == "null" || grabbedObject == null) return;

            Vector3 offset = Vector3.zero;

            // set offsets & rotations
            switch (currentItem)
            {
                case "Scythe":
                    offset = new Vector3(0, 0, 0);
                    grabbedObject.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(-90, 180, 90); break;

                case "Axe":
                    offset = new Vector3(0, 0, 0);
                    grabbedObject.transform.rotation = defaultToolPosition.rotation; break;

                case "Hoe":
                    offset = new Vector3(0, 0, 0);
                    grabbedObject.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, -90, 0); break;

                case "Bag":
                    offset = new Vector3(0, 0, 0);
                    grabbedObject.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, 0, 0); break;
                case "Seed Bag":
                    offset = new Vector3(0, 0, 0);
                    grabbedObject.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, 180, 0); break;

            }
            grabbedObject.transform.position = defaultToolPosition.position + offset;
        }
    }
}
