using UnityEngine;

public class Movement : MonoBehaviour
{
    private const float gravity = -9.81f;
    private const float jumpHeight = 0.5f;
    private const int playerSpeed = 2;


    private Vector3 velocity;
    private bool isJumping = false;
    private bool isGrounded;

    private void Update()
    {
        MoveCharacterController();
    }

    private void MoveCharacterController()
    {
        CharacterController controller = GetComponent<CharacterController>();

        isGrounded = controller.isGrounded;
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

        controller.Move(velocity * Time.deltaTime + Time.deltaTime * playerSpeed * move);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping != true)
            {
                isJumping = true;
            }
        }
    }
}
