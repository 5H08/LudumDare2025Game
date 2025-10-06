using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    private GameObject menu;

    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("UI Canvas").transform.GetChild(7).gameObject;
    }

    public void ItemPickup(string name, Sprite icon)
    {

    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
