using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator walkAnim;
    private Vector3 playerVelocity;

    public float walkSpeed = 4f;

    public float maxStamina = 5f; //Seconds
    public float stamina;
    private bool recoverStamina = false;
    public float recoverCooldown = 3f;
    public float recoverScale = 1.2f;
    private Coroutine currentCooldown;
    private Slider staminaBar;

    public float sprintSpeed = 7f;
    public bool sprinting = false;

    public float jumpHeight = 1f;
    public float gravity = -20f;
    public bool isGrounded;

    void Start()
    {
        stamina = maxStamina;
        staminaBar = GameObject.FindGameObjectWithTag("UI Canvas")
                     .transform.GetChild(2).GetChild(1).GetComponent<Slider>();
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
        controller = GetComponent<CharacterController>();
        walkAnim = GameObject.FindGameObjectWithTag("UI Canvas").transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void MovePlayer(Vector2 input)
    {
        if (input == Vector2.zero)
        {
            walkAnim.SetBool("Running", false);
        }
        else
        {
            walkAnim.SetBool("Running", true);
        }

        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        if (sprinting)
        {
            controller.Move(transform.TransformDirection(moveDir) * sprintSpeed * Time.deltaTime);
            stamina -= Time.deltaTime;
            if (stamina <= 0)
            {
                stamina = 0;
                ToggleSprint(false);
            }
            staminaBar.value = stamina;
        }
        else
        {
            controller.Move(transform.TransformDirection(moveDir) * walkSpeed * Time.deltaTime);
            if (recoverStamina && stamina < maxStamina)
            {
                stamina += Time.deltaTime * recoverScale;
                if (stamina > maxStamina)
                {
                    stamina = maxStamina;
                }
                staminaBar.value = stamina;
            }
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
            if (stamina > 0)
            {
                recoverStamina = false;
                sprinting = true;
            }
            else
            {
                sprinting = false;
            }
        }
        else
        {
            if (currentCooldown != null)
            {
                StopCoroutine(currentCooldown);
            }
            currentCooldown = StartCoroutine(RecoveryCooldown());
            sprinting = false;
        }

    }

    IEnumerator RecoveryCooldown()
    {
        recoverStamina = false;
        yield return new WaitForSeconds(recoverCooldown);
        recoverStamina = true;
    }
}
