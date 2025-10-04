using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputs input;
    private PlayerInputs.InGameActions inGame;

    private PlayerMovement movement;
    private PlayerCamera cam;
    private PlayerCombat combat;

    void Awake()
    {
        // Initialize input system
        input = new PlayerInputs();
        inGame = input.InGame;
        inGame.Enable();
        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Bind action methods to input
        inGame.Jump.performed += ctx => movement.Jump();
        inGame.Sprint.performed += ctx => movement.ToggleSprint(true);
        inGame.Sprint.canceled += ctx => movement.ToggleSprint(false);
        inGame.LeftPunch.performed += ctx => combat.LeftPunch();
        inGame.RightPunch.performed += ctx => combat.RightPunch();
    }

    void Start()
    {

        // Get all utility scripts
        movement = GetComponent<PlayerMovement>();
        cam = GetComponent<PlayerCamera>();
        combat = GetComponent<PlayerCombat>();

    }

    void Update()
    {
        movement.MovePlayer(inGame.Walk.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        cam.TurnPlayerCam(inGame.Camera.ReadValue<Vector2>());
    }
}
