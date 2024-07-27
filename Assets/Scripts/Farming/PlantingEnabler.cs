using UnityEngine;

public class PlantingEnabler : MonoBehaviour
{
    public bool plantingAllowed;

    private void Start()
    {
        plantingAllowed = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WheatBag") || collision.gameObject.CompareTag("CarrotBag"))
        {
            if (transform.childCount > 0)
            {
                plantingAllowed = false;
            }
            else if (transform.childCount <= 0)
            {
                plantingAllowed = true;
            }
        }
    }
}
