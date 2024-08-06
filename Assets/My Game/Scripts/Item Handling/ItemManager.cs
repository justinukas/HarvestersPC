using UnityEngine;

namespace Main.ItemHandling
{
    public class ItemManager : MonoBehaviour
    {
        [HideInInspector] public string currentTool = "null";
        [HideInInspector] public string currentPlant = "null";
        [HideInInspector] public GameObject grabbedTool;
        [HideInInspector] public GameObject grabbedPlant;
        [SerializeField] public Transform defaultToolPosition;
        [SerializeField] public Transform defaultPlantPosition;
        
        private ItemRaycast itemRaycast;
        private ItemPositionAndRotation itemPositionAndRotation;
        private ItemUse itemUse;
        private ItemDrop itemDrop;

        private void Start()
        {
            itemRaycast = GetComponent<ItemRaycast>();
            itemPositionAndRotation = GetComponent<ItemPositionAndRotation>();
            itemUse = GetComponent<ItemUse>();
            itemDrop = GetComponent<ItemDrop>();
        }

        private void Update()
        {
            itemRaycast.CheckRaycast(ref currentTool, ref grabbedTool, ref currentPlant, ref grabbedPlant);
            //itemPositionAndRotation.UpdateItemPositionAndRotation(currentTool, grabbedTool, currentPlant, grabbedPlant, defaultToolPosition, defaultPlantPosition);
            itemUse.UseItem(currentTool, grabbedTool, ref currentPlant, ref grabbedPlant);
            itemDrop.DropItem(ref currentTool, ref grabbedTool, ref currentPlant, ref grabbedPlant);
        }

        public void SetParents()
        {
            itemPositionAndRotation.UpdateItemPositionAndRotation(currentTool, grabbedTool, currentPlant, grabbedPlant, defaultToolPosition, defaultPlantPosition);
        }
    }
}
