using UnityEngine;

public class ItemInteractionsold : MonoBehaviour
{
    private Transform camTransform;

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

        //if (item == "null")
        //{
            //DefaultArmPositions();
        //}

        if (item == "Scythe")
        {
            Scythe();
        }

        if (item == "Axe")
        {
            Axe();
        }
    }

    // takes Grabbables layermask, aka the 6th layermask
    public LayerMask grabbables = 1 << 6;

    // raycast stuff
    private Ray ray;
    private RaycastHit hit;

    // the gameobject which the raycast hit
    private GameObject grabbedObject;

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
        item = "Scythe";
        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving
        grabbedObject.transform.Find("Scythe").localRotation = Quaternion.Euler(0, 0, 0); // reset scythe orientation
    }
    void GrabAxe()
    {
        item = "Axe";
        grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving
        grabbedObject.transform.Find("Axe").localRotation = Quaternion.Euler(0, 0, 0); // reset axe orientation
    }

    // hand gameobjects
    //public GameObject Arms;
    //public GameObject LHand;
    //public GameObject RHand;

    // sets the default arms positions
    //void DefaultArmPositions()
    //{
       // LHand.transform.localPosition = new Vector3(-0.15f, 0, 0);
        //RHand.transform.localPosition = new Vector3(0.15f, 0, 0);
        //Arms.transform.position = camTransform.position - new Vector3(0, 0.25f, 0) + camTransform.forward * 0.3f;
        //Arms.transform.rotation = Quaternion.LookRotation(camTransform.forward * 0.8f, camTransform.up * 0.8f);
    //}

    // drops equipped item
    void DropItem()
    {
        if (item == "Scythe")
        {
            //LHand.transform.localRotation = RHand.transform.localRotation = Quaternion.Euler(0, 0, 0);
            grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        item = "null";
    }

    public bool isSwinging = false;

    // swings scythe
    void Scythe()
    {
        // makes scythe go in front of the camera
        grabbedObject.transform.position = camTransform.position - new Vector3(0, 0.25f, 0) + camTransform.forward * 0.3f;
        grabbedObject.gameObject.transform.rotation = Quaternion.LookRotation(camTransform.forward * 0.8f, camTransform.up * 0.8f) * Quaternion.Euler(-90, 180, 90);

        // aligns hand position and rotation with scythe handles
        GameObject LHandle = grabbedObject.transform.Find("Scythe").Find("LHandle").gameObject;
        GameObject RHandle = grabbedObject.transform.Find("Scythe").Find("RHandle").gameObject;

        //LHand.transform.position = LHandle.transform.position;
        //LHand.transform.Find("LArm").transform.localPosition = new Vector3(0, 0, -0.09f);

        //RHand.transform.position = RHandle.transform.position;
        //RHand.transform.Find("RArm").transform.localPosition = new Vector3(0, 0, -0.09f);

        //LHand.transform.rotation = LHandle.transform.rotation;
        //RHand.transform.rotation = RHandle.transform.rotation;

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

    void Axe()
    {
        // makes axe go in front of the camera
        grabbedObject.transform.position = camTransform.position - new Vector3(0, 0.15f, 0) + camTransform.forward * 0.3f;
        grabbedObject.gameObject.transform.rotation = Quaternion.LookRotation(camTransform.forward * 0.8f, camTransform.up * 0.8f);

        // matches hand position and rotation with axe holding attachments
        GameObject LHandle = grabbedObject.transform.Find("Axe").Find("AttachmentLArm").gameObject;
        GameObject RHandle = grabbedObject.transform.Find("Axe").Find("AttachmentRArm").gameObject;

        //LHand.transform.position = LHandle.transform.position;
        //LHand.transform.Find("LArm").transform.localPosition = new Vector3(0, 0, -0.09f);

        //RHand.transform.position = RHandle.transform.position;
        //RHand.transform.Find("RArm").transform.localPosition = new Vector3(0, 0, -0.09f);

        //LHand.transform.rotation = LHandle.transform.rotation;
        //RHand.transform.rotation = RHandle.transform.rotation;

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
}























    //---------------------------------------------------------//
    //                                                         //
    //               scrapped but useful stuff                 //
    //                                                         //
    //---------------------------------------------------------//


    // debug for raycast

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, hit.point);
    }*/


    // in update method

    // scythe swinging animation stuff
    /*if (scytheAnim == 1)
    {
        // scythe retracts
        grabbedObject.transform.Find("Scythe").localRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 35), timeCounter); 
        timeCounter += Time.deltaTime * 4;
        if (timeCounter >= 1)
        {
            timeCounter = 0.0f;
            scytheAnim = 2;
        }
    }

    if (scytheAnim == 2)
    {
        // scythe swings
        grabbedObject.transform.Find("Scythe").localRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 35), Quaternion.Euler(0, 0, -85f), timeCounter); 
        timeCounter += Time.deltaTime * 3;

        if (timeCounter >= 1)
        {
            timeCounter = 0.0f;
            scytheAnim = 3;
        }
    }

    if (scytheAnim == 3)
    {
        // scythe returns to neutral position
        grabbedObject.transform.Find("Scythe").localRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, -85f), Quaternion.Euler(0, 0, 0), timeCounter); 
        timeCounter += Time.deltaTime * 2;
        if (timeCounter >= 1)
        {
            scytheAnim = 0;
            timeCounter = 0.0f;
            isSwingingScythe = false;
        }
    }*/

