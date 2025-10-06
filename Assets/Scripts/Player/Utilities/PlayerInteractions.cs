using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    private GameObject menu;
    public Dictionary<string, Sprite> items = new Dictionary<string, Sprite>();

    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("UI Canvas").transform.GetChild(4).gameObject;
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
}
