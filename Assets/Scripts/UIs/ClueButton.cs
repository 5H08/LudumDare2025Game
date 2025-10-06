using TMPro;
using UnityEngine;

public class ClueButton : MonoBehaviour
{
    private GameObject listHide;
    private GameObject sellPage;

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

    public void CheckObtained(string name)
    {
        if (name == itemName)
        {
            title.text = "[Sold Out]";
            price.text = "$----";
            desc.text = "Unavailable";
        }
    }
}
