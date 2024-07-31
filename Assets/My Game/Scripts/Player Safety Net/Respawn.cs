using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.transform.position = new Vector3(114f, 3f, 38f);
        if (collider.gameObject.name == "Player")
        {
            collider.gameObject.GetComponent<CharacterController>().enabled = false;
            collider.gameObject.transform.position = new Vector3(114f, 3f, 38f);
            collider.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
