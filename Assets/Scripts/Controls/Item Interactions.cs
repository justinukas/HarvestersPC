using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    public LayerMask layerMask = 1<<6; // takes Grabbables layer, aka the 6th layer

    private Ray ray;
    private RaycastHit hit;
    void Update()
    {
        ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("registered E input");
            if (Physics.Raycast(ray, out hit, 1.5f, layerMask))
            {
                Debug.Log("raycast hit  " + hit.collider.gameObject.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, hit.point);
    }
}
