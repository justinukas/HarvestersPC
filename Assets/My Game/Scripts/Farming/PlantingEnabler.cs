using System.Collections;
using UnityEngine;

namespace Main.Farming
{
    public class PlantingEnabler : MonoBehaviour
    {
        [HideInInspector]
        public bool plantingAllowed;

        private void Start()
        {
            StartCoroutine(CheckForChildren());
        }

        private IEnumerator CheckForChildren()
        {
            while (enabled)
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
            yield return new WaitForSeconds(0.1f);
        }
    }
}