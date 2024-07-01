using UnityEngine;

public class Movement : MonoBehaviour
{
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
        GetComponent<CharacterController>().Move(move);
        if (GetComponent<CharacterController>().isGrounded)
        {
            gravity = 0;
        }
    }
}
