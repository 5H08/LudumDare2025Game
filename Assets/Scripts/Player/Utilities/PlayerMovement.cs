using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    public float walkSpeed = 4f;

    public float stamina = 1f;
    public float sprintSpeed = 7f;
    public bool sprinting = false;

    public float jumpHeight = 2f;
    public float gravity = -20f;
    public bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void MovePlayer(Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        if (sprinting)
        {
            controller.Move(transform.TransformDirection(moveDir) * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(transform.TransformDirection(moveDir) * walkSpeed * Time.deltaTime);
        }
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * gravity * -3);
        }
    }

    public void ToggleSprint(bool toggleOn)
    {
        if (toggleOn)
        {
            sprinting = true;
            print("Start sprint");
        }
        else
        {
            sprinting = false;
            print("Stop sprint");
        }
    }
}
