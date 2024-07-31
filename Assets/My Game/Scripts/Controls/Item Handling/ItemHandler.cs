using UnityEngine;

namespace Main.Controls
{
    public class ItemHandler : MonoBehaviour
    {
        public bool isSwinging = false;
        public string currentItem = "null";
        public GameObject grabbedObject;
        public Transform defaultToolPosition;

        private ItemRaycast itemRaycast;
        private ItemDrop itemDrop;
        private ItemPositionAndRotation itemPositionAndRotation;
        private ItemUse itemUse;

        private void Start()
        {
            itemRaycast.CheckRaycast(ref currentItem, ref grabbedObject);
            itemDrop.DropItem(ref currentItem, ref isSwinging, ref grabbedObject);
            itemPositionAndRotation.UpdateItemPositionAndRotation(currentItem, grabbedObject, defaultToolPosition);
            itemUse.UseItem(ref currentItem, ref grabbedObject, ref isSwinging);
        }
    }
}