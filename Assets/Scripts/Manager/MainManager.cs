using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public List<string> items = new List<string>();

    public int score;
    private TMP_Text scoreDisplay;
    private GameObject warning;

    private GameObject endScreen;
    private InputManager input;
    public MusicManager music;

    private void Start()
    {
        score = 0;
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        scoreDisplay = GameObject.FindGameObjectWithTag("UI Canvas").transform.GetChild(5).
                       GetComponent<TMP_Text>();
        warning = GameObject.FindGameObjectWithTag("UI Canvas").transform.GetChild(7).gameObject;
        music = GetComponent<MusicManager>();
        endScreen = warning = GameObject.FindGameObjectWithTag("UI Canvas").transform.GetChild(8).gameObject;
    }

    public void SixtySeconds()
    {
        print("countdown");
        warning.SetActive(true);
        music.StartEndMusic();
    }

    public void EndRun(string results)
    {
        switch (results)
        {
            case "time":
                input.inMenu.Enable();
                input.inGame.Disable();
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endScreen.SetActive(true);
                TMP_Text endText = endScreen.transform.GetChild(0).GetComponent<TMP_Text>();
                endText.text = "GAME ENDED\r\n\r\nYOUR COLLECTOR SCORE IS:\r\n\r\n" + score + "\n\nThe time has ended.\n\n[Q] MENU    [X] EXIT";
                break;

            case "dead":
                input.inMenu.Enable();
                input.inGame.Disable();
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                endScreen.SetActive(true);
                TMP_Text endText2 = endScreen.transform.GetChild(0).GetComponent<TMP_Text>();
                endText2.text = score + "GAME ENDED\r\n\r\nYOUR COLLECTOR SCORE IS:\r\n\r\n" + score + "\n\nYOU DIED.\n\n[Q] MENU    [X] EXIT";
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
