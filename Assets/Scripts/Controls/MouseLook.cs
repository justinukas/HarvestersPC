using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;

    public float mouseSpeed = 1f;

    private void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
        transform.eulerAngles = new Vector2 (0, rotation.y * mouseSpeed);
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * mouseSpeed, 0, 0);
    }
}
