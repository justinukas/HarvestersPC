using UnityEngine;

namespace Main.Controls
{
    public class ItemHandler : MonoBehaviour
    {
        public bool isSwinging = false;
        public string currentItem = "null";
        public GameObject grabbedObject;
        

        private ItemRaycast itemRaycast;
        private ItemDrop itemDrop;
        private ItemPositionAndRotation itemPositionAndRotation;
        private ItemUse itemUse;



        private void Start()
        {
            itemRaycast = GetComponent<ItemRaycast>();
            itemDrop = GetComponent<ItemDrop>();
            itemPositionAndRotation = GetComponent<ItemPositionAndRotation>();
            itemUse = GetComponent<ItemUse>();
        }

        private void Update()
        {
            itemRaycast.CheckRaycast(ref currentItem, ref grabbedObject);
            itemDrop.DropItem(ref currentItem, ref isSwinging, ref grabbedObject);
            itemUse.UseItem(ref currentItem, ref grabbedObject, ref isSwinging);
        }

        private void LateUpdate()
        {
            if (grabbedObject != null)
            {
                itemPositionAndRotation.UpdateItemPositionAndRotation(currentItem, grabbedObject);
            }
        }
    }
}
