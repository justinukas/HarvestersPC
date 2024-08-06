using UnityEngine;

namespace Main.Controls
{
    public class MouseLook : MonoBehaviour
    {
        Vector2 mousePosition;

        public float sensitivity = 1f;

        private void Start()
        {
            mousePosition.y = 90;
        }

        private void Update()
        {
            ControlCamera();
        }

        // this scuffed af but it works idk
        private void ControlCamera()
        {
            mousePosition.x -= Input.GetAxis("Mouse Y") * sensitivity;
            mousePosition.y += Input.GetAxis("Mouse X") * sensitivity;

            mousePosition.x = Mathf.Clamp(mousePosition.x, -90f, 90f);

            transform.localRotation = Quaternion.Euler(0, mousePosition.y, 0);
            Camera.main.transform.localRotation = Quaternion.Euler(mousePosition.x, 0, 0);
        }
    }
}
