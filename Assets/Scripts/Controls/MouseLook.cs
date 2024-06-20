using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;

    public float mouseSpeed = 1f;

    private bool locked = false; // lock toggle
    private float cd; // keypress cooldown

    private void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
        transform.eulerAngles = new Vector2 (0, rotation.y * mouseSpeed);
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * mouseSpeed, 0, 0);


        // curse lock toggle
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (locked == true && cd >= 0.2f)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                locked = false;
                cd = 0f;
            }
            if (locked == false && cd >= 0.2f)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                locked = true;
                cd = 0f;
            }
        }
        cd += Time.deltaTime;
    }
}
