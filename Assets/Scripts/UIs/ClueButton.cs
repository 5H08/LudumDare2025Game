using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClueButton : MonoBehaviour
{
    private GameObject listHide;
    private GameObject sellPage;

    private MainManager manager;

    public string sellTitle;
    private TMP_Text title;
    public string sellDescription;
    private TMP_Text desc;
    public string sellPrice;
    private TMP_Text price;

    public string itemName;

    private void Start()
    {
        listHide = transform.parent.parent.parent.gameObject;
        sellPage = transform.parent.parent.parent.parent.GetChild(2).gameObject;
        title = transform.GetChild(0).GetComponent<TMP_Text>();
        price = transform.GetChild(1).GetComponent<TMP_Text>();
        desc = transform.GetChild(2).GetComponent<TMP_Text>();
        title.text = sellTitle;
        price.text = sellPrice;
        desc.text = sellDescription;

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MainManager>();
    }

    private void OnEnable()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MainManager>();
        foreach (string item in manager.items)
        {
            if (CheckObtained(item))
            {
                break;
            }
        }
    }

    public void OpenDesc()
    {
        listHide.SetActive(false);
        sellPage.SetActive(true);
        Transform page = sellPage.transform;
        page.GetChild(0).GetComponent<TMP_Text>().text = sellTitle;
        page.GetChild(1).GetComponent<TMP_Text>().text = sellDescription;
        page.GetChild(2).GetComponent<TMP_Text>().text = sellPrice;
    }

    public bool CheckObtained(string name)
    {
        if (name == itemName)
        {
            GetComponent<Button>().interactable = false;
            title.text = "[Sold Out]" + sellTitle;
            price.text = "$------";
            desc.text = "[Unavailable]" + sellDescription;
            return true;
        }
        return false;
    }
}
