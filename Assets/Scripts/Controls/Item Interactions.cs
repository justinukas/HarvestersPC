using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    public bool isSwinging = false;

    // object distinguishing
    private string currentItem = "null";
    private GameObject grabbedObject; // gameobject that the raycast hit

    // transform for default tool position
    public Transform defaultToolPosition;

    private void Update()
    {
        ItemInteractionCheck();
        ItemDropCheck();
        ItemPositionAndRotation();
        ItemSwing();
    }

    // checks for raycast collision within 1.5 units and in front of camera on the press of E
    private void ItemInteractionCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, 1 << 6 /* takes 6th (grabbables) layermask */) && currentItem == "null")
            {
                grabbedObject = hit.collider.gameObject;
                string itemName = grabbedObject.name;

                if (itemName == "Scythe" || itemName == "Axe" || itemName == "Hoe")
                {
                    currentItem = itemName;

                    grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving
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
            grabbedObject.transform.Find(currentItem).GetComponent<Animator>().Play("DefaultState");
            isSwinging = false;
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
                offset = new Vector3(0, 0, 0);
                break;
            case "Axe":
                offset = new Vector3(0, 0, 0);
                break;
            case "Hoe":
                offset = new Vector3(0, 0, 0);
                break;
        }
        defaultToolPosition.position = defaultToolPosition.position + offset;
        grabbedObject.transform.position = defaultToolPosition.position;

        // set rotations
        switch (currentItem)
        {
            case "Scythe":
                grabbedObject.gameObject.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(-90, 180, 90);
                break;
            case "Axe":
                grabbedObject.gameObject.transform.rotation = defaultToolPosition.rotation;
                break;
            case "Hoe":
                grabbedObject.gameObject.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, -90, 0);
                break;
        }
    }


    private void ItemSwing()
    {
        if (currentItem == "null" || grabbedObject == null) return;

        Animator currentAnimator = grabbedObject.transform.Find(currentItem).GetComponent<Animator>();
        if (Input.GetMouseButtonDown(0))
        {
            if (currentItem == "Scythe" || currentItem == "Axe" || currentItem == "Hoe")
            {
                if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"{currentItem} Swing") == false)
                {
                    currentAnimator.Play($"{currentItem} Swing");
                }
            }
            if (currentItem == "Bag")
            {
                // do stuff
            }
        }

        if (currentItem == "Scythe" || currentItem == "Axe" || currentItem == "Hoe")
        { isSwinging = currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"{currentItem} Swing"); }

        if (currentItem == "Bag")
        { /*play anim */}
    }
}
