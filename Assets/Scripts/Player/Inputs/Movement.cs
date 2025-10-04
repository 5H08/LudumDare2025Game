using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    public float walkSpeed = 4f;
    public float jumpHeight = 2f;
    public float gravity = -20f;
    public bool isGrounded;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer(Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        controller.Move(transform.TransformDirection(moveDir) * walkSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
