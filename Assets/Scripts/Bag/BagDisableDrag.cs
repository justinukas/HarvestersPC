using UnityEngine;

public class BagDisableDrag : MonoBehaviour
{
    [SerializeField] BagToPlayer BagToPlayer;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Selling Hitbox")
        {
            gameObject.GetComponent<Rigidbody>().drag = 1;
            gameObject.GetComponent<Rigidbody>().angularDrag = 1;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (BagToPlayer.move == false)
        {
            gameObject.GetComponent<Rigidbody>().drag = 0;
            gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        }
    }
}
