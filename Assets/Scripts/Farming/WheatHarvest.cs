using UnityEngine;

public class WheatHarvest : MonoBehaviour
{
    [SerializeField] BagInventory BagInventory;
    [SerializeField] ToolInteractions ToolInteractions;

    private void OnCollisionEnter(Collision collider)
    {
        foreach (ContactPoint contactPoint in collider.contacts) // this foreach is for checking for the correct local collider, the head of the scythe
        {
            WheatDestruction WheatDestruction = contactPoint.otherCollider.gameObject.GetComponent<WheatDestruction>();
            WheatGrowth WheatGrowth = contactPoint.otherCollider.gameObject.GetComponent<WheatGrowth>();

            if (collider.gameObject.CompareTag("WheatSmall") && contactPoint.thisCollider.gameObject.name == "Head" && BagInventory.isBagOpen == true &&
                WheatGrowth.isHarvestable == true && ToolInteractions.isSwinging == true)
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
