using System.Collections;
using UnityEngine;

public class PlantingEnabler : MonoBehaviour
{
    public bool plantingAllowed;

    private void Start()
    {
        StartCoroutine(CheckForChildren());
        plantingAllowed = false;
    }

    private IEnumerator CheckForChildren()
    {
        while (enabled)
        {
            if (transform.Find("WheatParent").childCount == 0 && transform.Find("CarrotParent").childCount == 0)
            {
                GetComponent<BoxCollider>().enabled = false;
                plantingAllowed = true;
                GetComponent<BoxCollider>().enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
