using UnityEngine;

public class Movement : MonoBehaviour
{
    private int playerSpeed = 2;

    private float x;
    private float z;

    private float gravity;

    private float jump;
    private float jumpForce;
    private float jumpDuration;

    private bool isJumping = false;

    private Vector3 move;

    private bool isGrounded;

    void Update()
    {
        Debug.Log(isJumping);


        isGrounded = GetComponent<CharacterController>().isGrounded;

        gravity -= 9.81f * Time.deltaTime;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        if (isJumping == false)
        {
            move = transform.right * x * playerSpeed * Time.deltaTime + transform.forward * z * playerSpeed * Time.deltaTime + transform.up * gravity;
        }

        if (isJumping == true)
        {
            move = transform.right * x * playerSpeed * Time.deltaTime + transform.forward * z * playerSpeed * Time.deltaTime + transform.up * jumpForce;
        }
        
        GetComponent<CharacterController>().Move(move);
        if (isGrounded)
        {
            gravity = 0;
        }

        if (isJumping == true && isGrounded)
        {
            gravity = 0;
            //jumpForce += 1f * Time.deltaTime;
            jump += 10 * Time.deltaTime;
            jumpDuration += Time.deltaTime;

        }
        if (jumpDuration > 0.5f)
        {
            isJumping = false;
            jumpDuration = 0f;
            jumpForce = 0f;
            jump = 0f;
        }
    }

    public void Jump()
    {
        isJumping = true;
    }
}
