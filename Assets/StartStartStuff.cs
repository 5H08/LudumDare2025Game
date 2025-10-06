using System.Collections;
using UnityEngine;

public class StartStartStuff : MonoBehaviour
{
    public AudioClip sorry;
    bool knocked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
