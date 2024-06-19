using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    // takes Grabbables layermask, aka the 6th layermask
    public LayerMask layerMask = 1<<6; 

    // raycast stuff
    private Ray ray;
    private RaycastHit hit;

    // the gameobject which the raycast hit
    private GameObject grabbedObject;

    // hand gameobjects
    public GameObject LHand;
    public GameObject RHand;

    // scythe specific variables
    public bool isSwingingScythe = false;
    private bool objectIsScythe = false;
    private int scytheAnim = 0; // animation counter

    private float timeCounter = 0.0f;
    void Update()
    {
        // checks for raycast collision within 1.5 units and in front of camera on the press of E. On raycast hit, calls GrabItem() method
        ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, 1.5f, layerMask))
            {
                GrabItem();
                grabbedObject = hit.collider.gameObject;
            }
        }

        if (objectIsScythe == true)
        {
            // makes scythe go in front of the camera
            grabbedObject.transform.position = Camera.main.transform.position - new Vector3 (0, 0.2f, 0) + Camera.main.transform.forward * 0.5f;
            grabbedObject.gameObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward * 0.8f, Camera.main.transform.up * 0.8f) * Quaternion.Euler(-90, 180, 90);
            
            // aligns hand position and rotation with scythe handles
            GameObject LHandle = grabbedObject.transform.Find("Scythe").Find("LHandle").gameObject;
            GameObject RHandle = grabbedObject.transform.Find("Scythe").Find("RHandle").gameObject;

            LHand.transform.position = LHandle.transform.position;
            RHand.transform.position = RHandle.transform.position;

            LHand.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward * 0.8f, Camera.main.transform.up * 0.8f);
            RHand.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward * 0.8f, Camera.main.transform.up * 0.8f);

            // checks for left mouse input and if anim isnt playing
            if (Input.GetMouseButtonDown(0) && scytheAnim == 0)
            {
                scytheAnim = 1; // starts swinging animation
                isSwingingScythe = true;
            }

            // scythe swinging animation stuff
            if (scytheAnim == 1)
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
            }
        }
    }
    void GrabItem()
    {
        if (hit.collider.gameObject.name == "ScytheParent")
        {
            hit.collider.transform.Find("Scythe").localRotation = Quaternion.Euler(0, 0, 0);
            hit.collider.transform.Find("Scythe").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
            objectIsScythe = true;
        }
    }

    // debug for raycast
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, hit.point);
    }*/
}
