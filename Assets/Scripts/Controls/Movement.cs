using UnityEngine;

public class Movement : MonoBehaviour
{
    private readonly float gravity = -9.81f;

    private Vector3 velocity;
    private int playerSpeed = 2;
    private float jumpHeight = 0.5f;
    private bool isJumping = false;
    private bool isGrounded;

    void Update()
    {
        isGrounded = GetComponent<CharacterController>().isGrounded;
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        

        if (isJumping == true && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            isJumping = false;
        }

        velocity.y += gravity * Time.deltaTime;

        GetComponent<CharacterController>().Move(velocity * Time.deltaTime + Time.deltaTime * playerSpeed * move);
    }

    public void Jump()
    {
        if (isJumping != true)
        {
            isJumping = true;
        }
    }

    /*private void Sprint()
    {

    }*/
}
