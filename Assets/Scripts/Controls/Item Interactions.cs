using UnityEngine;

public class ItemInteractions : MonoBehaviour
{

    public bool isSwinging = false;

    // object distinguishing
    public string currentItem = "null";
    public GameObject grabbedObject;

    // transform for default tool position
    public Transform defaultToolPosition;

    private void Update()
    {
        ItemRaycast();
        ItemDrop();
        ItemPositionAndRotation();
        ItemUse();
    }

    // checks for raycast collision within 1.5 units and in front of camera on the press of E
    private void ItemRaycast()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, 1 << 6 /* takes 6th (grabbables) layermask */) && currentItem == "null")
            {
                grabbedObject = hit.collider.gameObject;

                if (grabbedObject.name == "Scythe" || grabbedObject.name == "Axe" || grabbedObject.name == "Hoe" || grabbedObject.name == "Bag")
                {
                    currentItem = grabbedObject.name;

                    grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition; // lock rigidbody from moving
                }
            }
        }
    }

    // drops equipped item
    private void ItemDrop()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentItem != "null")
        {
            grabbedObject.transform.Find(currentItem).GetComponent<Animator>().Play("DefaultState");
            grabbedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            isSwinging = false;
            currentItem = "null";
            grabbedObject = null;
        }
    }

    private void ItemPositionAndRotation()
    {
        if (currentItem == "null" || grabbedObject == null) return;

        Vector3 offset = Vector3.zero;
        
        // set offsets & rotations
        switch (currentItem)
        {
            case "Scythe":
                offset = new Vector3(0, 0, 0);
                grabbedObject.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(-90, 180, 90); break;

            case "Axe":
                offset = new Vector3(0, 0, 0);
                grabbedObject.transform.rotation = defaultToolPosition.rotation; break;

            case "Hoe":
                offset = new Vector3(0, 0, 0); 
                grabbedObject.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(0, -90, 0); break;

            case "Bag":
                offset = new Vector3(0, 0, 0);
                grabbedObject.transform.rotation = defaultToolPosition.rotation * Quaternion.Euler(-90, 180, 180); break;

        }
        grabbedObject.transform.position = defaultToolPosition.position + offset;

    }

    private void ItemUse()
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
            else if (currentItem == "Bag")
            {
               if (currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Charge Up Bag Throw") == false && GameObject.Find("Throwing Charge Bar").transform.Find("Canvas").GetComponent<CanvasGroup>().alpha == 1)
                {
                    currentAnimator.Play("Charge Up Bag Throw");
                }
            }
        }

        if (currentItem == "Scythe" || currentItem == "Axe" || currentItem == "Hoe")
        { isSwinging = currentAnimator.GetCurrentAnimatorStateInfo(0).IsName($"{currentItem} Swing"); }
    }
}
