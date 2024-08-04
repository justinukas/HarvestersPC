using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemManager : MonoBehaviour
    {
        [HideInInspector] public string currentItem = "null";
        [HideInInspector] public string currentPlant = "null";
        [HideInInspector] public GameObject grabbedTool;
        [HideInInspector] public GameObject grabbedPlant;
        
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
            itemRaycast.CheckRaycast(ref currentItem, ref grabbedTool, ref currentPlant, ref grabbedPlant);
            itemDrop.DropItem(ref currentItem, ref grabbedTool, ref currentPlant, ref grabbedPlant);
            itemUse.UseItem(currentItem, grabbedTool, grabbedPlant);
        }

        private void LateUpdate()
        {
            itemPositionAndRotation.UpdateItemPositionAndRotation(currentItem, grabbedTool, currentPlant, grabbedPlant);
        }
    }
}
