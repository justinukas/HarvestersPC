using UnityEngine;

namespace Main.Controls
{
    public class MouseLook : MonoBehaviour
    {
        Quaternion rotation;

        public float mouseSpeed = 1f;

        private void LateUpdate()
        {
            ControlMouse();
        }

        private void ControlMouse()
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
            transform.rotation = Quaternion.Euler(0, rotation.y * mouseSpeed, 0);
            Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * mouseSpeed, 0, 0);
        }
    }
}
