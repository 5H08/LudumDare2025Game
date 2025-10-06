using UnityEngine;

public class CluePageExit : MonoBehaviour
{
    private GameObject cluePage;
    private GameObject clueList;

    private void Start()
    {
        cluePage = transform.parent.gameObject;
        clueList = transform.parent.parent.GetChild(1).gameObject;
    }

    public void ExitPage()
    {
        cluePage.SetActive(false);
        clueList.SetActive(true);
    }
}
