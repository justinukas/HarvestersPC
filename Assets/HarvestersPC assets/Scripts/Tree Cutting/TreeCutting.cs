using UnityEngine;

public class TreeCutting : MonoBehaviour
{
    private AudioSource soundEffect;
    private ParticleSystem woodParticles;

    private float cooldown = 0.5f;
    private bool canCut = true;

    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        woodParticles = transform.Find("Head").Find("Particles").GetComponent<ParticleSystem>();
    }

    // cooldown timer method
    private void Update()
    {
        cooldown -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collider)
    {
        foreach (ContactPoint contactPoint in collider.contacts) // this foreach is for checking if the collider colliding is the one of the axe's head
        {
            if (collider.gameObject.CompareTag("Tree") && cooldown <= 0 && canCut == true && contactPoint.thisCollider.gameObject.name == "Cube" && !gameObject.CompareTag("UnboughtTool"))
            {
                canCut = false;
                cooldown = 0.5f;

                GameObject tree = collider.gameObject;

                TimesCut timesCut = tree.GetComponent<TimesCut>();
                ObjDestruction treeDestruction = tree.GetComponent<ObjDestruction>();

                gameObject.GetComponent<AudioSource>().Play();
                gameObject.GetComponentInChildren<ParticleSystem>().Emit(20);

                timesCut.timesCut += 1;

                if (timesCut.timesCut >= 3)
                {
                    FellTree(tree, treeDestruction);
                }

                PlaySoundAndEmitParticles();
            }
        }
    }

    // makes axe be able to cut again if it leaves the tree hitbox
    private void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.CompareTag("Tree"))
        {
            canCut = true;
        }
    }

    private void FellTree(GameObject tree, ObjDestruction destructionScript)
    {
        // make apples fall down if its an apple tree
        if (tree.name == "Apple Tree")
        {
            tree.GetComponent<AppleFall>().UnparentApples();
        }

        tree.tag = "Untagged";

        // make the tree fall in the direction of the front of the axe
        Rigidbody treeRb = tree.GetComponent<Rigidbody>();
        treeRb.isKinematic = false;
        treeRb.AddForce(transform.forward * 2);

        // destroy tree after 2s
        destructionScript.DestroyObject(2f);
    }

    private void PlaySoundAndEmitParticles()
    {
        soundEffect.Play();
        woodParticles.Emit(20);
    }
}
