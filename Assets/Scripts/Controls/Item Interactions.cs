using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    public bool isSwinging = false;

    // object distinguishing
    private string currentItem = "null";
    private GameObject grabbedObject; // gameobject that the raycast hit

    private Transform camTransform;
    private void Start()
    {
        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        ItemInteractionCheck();
        ItemDropCheck();
        ItemPositionAndRotation();
        ItemSwing();
    }

    // raycast stuff
    private Ray ray;
    private RaycastHit hit;

    // checks for raycast collision within 1.5 units and in front of camera on the press of E
    private void ItemInteractionCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ray = new Ray(camTransform.position, camTransform.forward);
            if (Physics.Raycast(ray, out hit, 1.5f, 1 << 6 /* takes 6th (grabbables) layermask */) && currentItem == "null")
            {
                grabbedObject = hit.collider.gameObject;
                string itemName = grabbedObject.name;

                if (itemName == "Scythe" || itemName == "Axe" || itemName == "Hoe")
                {
                    grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving
                    currentItem = itemName;
                }
            }
        }
    }

    private void ItemDropCheck()
    {
        // drops equipped item
        if (Input.GetKeyDown(KeyCode.R) && currentItem != "null")
        {
            grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            currentItem = "null";
            grabbedObject = null;
        }
    }


    private void ItemPositionAndRotation()
    {
        if (currentItem == "null" || grabbedObject == null) return;

        Vector3 offset = Vector3.zero;
        
        // set offsets
        switch (currentItem)
        {
            case "Scythe":
                offset = new Vector3(0, -0.25f, 0);
                break;
            case "Axe":
                offset = new Vector3(0, -0.15f, 0);
                break;
            case "Hoe":
                offset = new Vector3(0, -0.2f, 0);
                break;
        }
        grabbedObject.transform.position = camTransform.position + offset + camTransform.forward * 0.3f;


        // set rotations
        switch (currentItem)
        {
            case "Scythe":
                grabbedObject.gameObject.transform.rotation = Quaternion.LookRotation(camTransform.forward * 0.8f, camTransform.up * 0.8f) * Quaternion.Euler(-90, 180, 90);
                break;
            case "Axe":
                grabbedObject.gameObject.transform.rotation = Quaternion.LookRotation(camTransform.forward * 0.8f, camTransform.up * 0.8f);
                break;
            case "Hoe":
                grabbedObject.gameObject.transform.rotation = Quaternion.LookRotation(camTransform.forward * 0.8f, camTransform.up * 0.8f) * Quaternion.Euler(0, -90, 0);
                break;
        }
    }


    private void ItemSwing()
    {
        if (currentItem == "null" || grabbedObject == null) return;

        Animator currentAnimator = grabbedObject.transform.Find(currentItem).GetComponent<Animator>();
        if (Input.GetMouseButtonDown(0))
        {
            if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"{currentItem} Swing") == false)
            {
                currentAnimator.Play($"{currentItem} Swing");
            }
        }

        isSwinging = currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"{currentItem} Swing");
    }
}
