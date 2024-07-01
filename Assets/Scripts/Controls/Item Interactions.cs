using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    private Transform camTransform;

    public bool isSwinging = false;

    // takes Grabbables layermask, aka the 6th layermask
    public LayerMask grabbables = 1 << 6;

    // raycast stuff
    private Ray ray;
    private RaycastHit hit;

    // the gameobject which the raycast hit
    private GameObject grabbedObject;

    // object distinguishing
    private string item = "null";

    void Update()
    {
        camTransform = Camera.main.transform;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            CastRay();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DropItem();
        }

        if (item == "Scythe")
        {
            Scythe();
        }

        if (item == "Axe")
        {
            Axe();
        }
    }

    // checks for raycast collision within 1.5 units and in front of camera on the press of E. On raycast hit, calls GrabItem() method
    void CastRay()
    {
        ray = new Ray(camTransform.position, camTransform.forward);
        if (Physics.Raycast(ray, out hit, 1.5f, grabbables) && item == "null")
        {
            grabbedObject = hit.collider.gameObject;
            GrabItem();
        }
    }

    // grabs item
    void GrabItem()
    {
        if (hit.collider.gameObject.name == "ScytheParent")
        {
            GrabScythe();
        }

        if (hit.collider.gameObject.name == "AxeParent")
        {
            GrabAxe();
        }
    }


    void GrabScythe()
    {
        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving
        item = "Scythe";
    }

    void Scythe()
    {
        // makes scythe go in front of the camera
        grabbedObject.transform.position = camTransform.position - new Vector3(0, 0.25f, 0)  + camTransform.forward * 0.3f;
        grabbedObject.gameObject.transform.rotation = Quaternion.LookRotation(camTransform.forward * 0.8f, camTransform.up * 0.8f) * Quaternion.Euler(-90, 180, 90);

        if (Input.GetMouseButtonDown(0))
        {
            // check if Scythe Swing anim is the one currently playing
            if (grabbedObject.transform.Find("Scythe").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Scythe Swing") == false) 
            {
                grabbedObject.transform.Find("Scythe").GetComponent<Animator>().Play("Scythe Swing");
            }
        }

        if (grabbedObject.transform.Find("Scythe").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Scythe Swing") == true)
        {
            isSwinging = true;
        }
        else { isSwinging = false; }
    }


    void GrabAxe()
    {
        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving
        item = "Axe"; 
    }
    void Axe()
    {
        // makes axe go in front of the camera
        grabbedObject.transform.position = camTransform.position - new Vector3(0, 0.15f, 0) + camTransform.forward * 0.3f;
        grabbedObject.gameObject.transform.rotation = Quaternion.LookRotation(camTransform.forward * 0.8f, camTransform.up * 0.8f);

        if (Input.GetMouseButtonDown(0))
        {
            // check if Scythe Swing anim is the one currently playing
            if (grabbedObject.transform.Find("Axe").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Axe Swing") == false)
            {
                grabbedObject.transform.Find("Axe").GetComponent<Animator>().Play("Axe Swing");
            }
        }

        if (grabbedObject.transform.Find("Axe").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Axe Swing") == true)
        {
            isSwinging = true;
        }
        else { isSwinging = false; }
    }



    // drops equipped item
    void DropItem()
    {
        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        item = "null";
    }
}
