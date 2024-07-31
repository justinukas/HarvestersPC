using UnityEngine;

public class TreeCutting : MonoBehaviour
{
    private float cooldown = 0.5f;

    private bool canCut = true;

    private GameObject Tree;


    // cooldown timer method
    private void Update()
    {
        cooldown -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collider)
    {
        foreach (ContactPoint contactPoint in collider.contacts) // this foreach is for checking if the collider colliding is the one of the axe's head
        {
            if (collider.gameObject.CompareTag("Tree") && cooldown <= 0 && canCut == true && contactPoint.thisCollider.gameObject.name == "Cube")
            {
                canCut = false;
                cooldown = 0.5f;

                Tree = collider.gameObject;

                TimesCut TimesCut = Tree.GetComponent<TimesCut>();
                TreeDestruction TreeDestruction = Tree.GetComponent<TreeDestruction>();

                gameObject.GetComponent<AudioSource>().Play();
                gameObject.GetComponentInChildren<ParticleSystem>().Emit(20);

                TimesCut.timesCut += 1;

                if (TimesCut.timesCut >= 3)
                {

                    Tree.tag = "Untagged";

                    Tree.GetComponent<Rigidbody>().isKinematic = false;
                    Tree.GetComponent<Rigidbody>().AddForce(transform.position.x + 4f, transform.position.y + 4f, transform.position.z + 4f);
                    StartCoroutine(TreeDestruction.DestroyTree());
                }
            }
        }
    }
    private void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.CompareTag("Tree"))
        {
            canCut = true;
        }
    }
}
