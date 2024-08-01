using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] Camera MainCamera;
    private void LateUpdate()
    {
        transform.forward = MainCamera.transform.forward;
    }
}
