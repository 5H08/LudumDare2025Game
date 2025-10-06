using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputs input;
    public PlayerInputs.InGameActions inGame;
    public PlayerInputs.InMenuActions inMenu;

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerCamera cam;
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private PlayerInteractions interactions;

    void Awake()
    {
        // Initialize input system
        input = new PlayerInputs();
        inGame = input.InGame;
        inGame.Enable();
        inMenu = input.InMenu;
        inMenu.Disable();
        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        // Get all utility scripts
        movement = GetComponent<PlayerMovement>();
        cam = GetComponent<PlayerCamera>();
        combat = GetComponent<PlayerCombat>();
        interactions = GetComponent<PlayerInteractions>();
        // Bind action methods to input
        inGame.Jump.performed += ctx => movement.Jump();
        inGame.Sprint.performed += ctx => movement.ToggleSprint(true);
        inGame.Sprint.canceled += ctx => movement.ToggleSprint(false);
        inGame.LeftPunch.performed += ctx => combat.LeftPunch();
        inGame.RightPunch.performed += ctx => combat.RightPunch();
        inGame.OpenMenu.performed += ctx => interactions.OpenMenu();
        // Menu
        inMenu.CloseMenu.performed += ctx => interactions.CloseMenu();
        inMenu.ReturnMenu.performed += ctx => interactions.ReturnMenu();
        inMenu.ExitGame.performed += ctx => interactions.ExitGame();
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
