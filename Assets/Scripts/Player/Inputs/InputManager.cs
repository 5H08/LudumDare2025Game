using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputs input;
    private PlayerInputs.InGameActions inGame;

    private PlayerMovement movement;
    private PlayerCamera cam;
    private PlayerCombat combat;

    void Start()
    {
        // Initialize input system
        input = new PlayerInputs();
        inGame = input.InGame;
        // Get all utility scripts
        movement = GetComponent<PlayerMovement>();
        cam = GetComponent<PlayerCamera>();
        combat = GetComponent<PlayerCombat>();
        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

    public void MovePlayer()
    {

    }

    public void Jump()
    {

    }
}
