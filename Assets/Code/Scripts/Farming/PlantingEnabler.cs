using UnityEngine;

namespace Main.Farming
{
    public class PlantingEnabler : MonoBehaviour
    {
        [HideInInspector] public bool plantingAllowed;

        private void Start()
        {
            if (transform.Find("WheatParent").childCount == 0 && transform.Find("CarrotParent").childCount == 0)
            {
                plantingAllowed = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Seed Bag"))
            {
                if (transform.Find("WheatParent").childCount > 0 || transform.Find("CarrotParent").childCount > 0)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    plantingAllowed = false;
                    GetComponent<BoxCollider>().enabled = true;
                }
                else if (transform.Find("WheatParent").childCount == 0 && transform.Find("CarrotParent").childCount == 0)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    plantingAllowed = true;
                    GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
    }
}
