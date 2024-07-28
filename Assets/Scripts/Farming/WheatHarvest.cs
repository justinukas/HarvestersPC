using Main.Controls;
using Main.Bag;
using UnityEngine;

namespace Main.Farming
{
    public class WheatHarvest : MonoBehaviour
    {    
        [SerializeField] BagInventory BagInventory; 
        [SerializeField] ToolInteractions ToolInteractions;
   
        private void OnCollisionEnter(Collision collider)
        {  
            foreach (ContactPoint contactPoint in collider.contacts) // this foreach is for checking for the correct local collider, the head of the scythe   
            {   
                WheatDestruction WheatDestruction = contactPoint.otherCollider.gameObject.GetComponent<WheatDestruction>();  
                Harvestability Harvestability = contactPoint.otherCollider.gameObject.GetComponent<Harvestability>();
 
                if (collider.gameObject.CompareTag("WheatSmall") && contactPoint.thisCollider.gameObject.name == "Head" && BagInventory.isBagOpen == true &&
                    Harvestability.isHarvestable == true && ToolInteractions.isSwinging == true)
                {  
                    collider.gameObject.tag = "HarvestedWheat";
                    collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                    gameObject.GetComponent<AudioSource>().Play();
                   
                    BagInventory.WheatCollection();
                    WheatDestruction.InvokeWheatDestruction();
                    
                }
                
            }
            
        }
        
    }
}
