using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public List<string> items = new List<string>();

    public int score;
    private TMP_Text scoreDisplay;

    private GameObject endScreen;
    private InputManager input;

    private void Start()
    {
        score = 0;
        scoreDisplay = GameObject.FindGameObjectWithTag("UI Canvas").transform.GetChild(5).
                       GetComponent<TMP_Text>();
    }

    public void EndRun(string results)
    {
        switch (results)
        {
            case "time":
                input.inMenu.Disable();
                input.inGame.Disable();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endScreen.SetActive(true);
                break;

            case "exit":
                input.inMenu.Disable();
                input.inGame.Disable();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endScreen.SetActive(true);
                break;

            default:
                input.inMenu.Disable();
                input.inGame.Disable();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endScreen.SetActive(true);
                break;
        }
    }

    public void AdjustScore(int amount)
    {
        score += amount;
        scoreDisplay.text = "Score: " + score;
    }

    public void AddItem(string itemName)
    {
        items.Add(itemName);
    }
}
