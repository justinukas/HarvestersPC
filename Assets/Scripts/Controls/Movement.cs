using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public int playerSpeed = 2;
    public float x;
    public float z;
    private float gravity;
    void Update()
    {
        gravity -= 9.81f * Time.deltaTime;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * x * playerSpeed * Time.deltaTime + transform.forward * z * playerSpeed * Time.deltaTime + transform.up * gravity;
        characterController.Move(move);
        if (characterController.isGrounded)
        {
            gravity = 0;
        }
    }
}
