using UnityEngine;

public class PhoneMenu : MonoBehaviour
{
    [SerializeField] private InputManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
    }

    private void OnEnable()
    {
        manager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        Time.timeScale = 0f;
        manager.inGame.Disable();
        manager.inMenu.Enable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        manager.inGame.Enable();
        manager.inMenu.Disable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
