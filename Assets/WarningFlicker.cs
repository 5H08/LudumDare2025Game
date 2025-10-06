using System.Collections;
using TMPro;
using UnityEngine;

public class WarningFlicker : MonoBehaviour
{
    private TMP_Text text;
    IEnumerator Flicker()
    {
        while (true)
        {
            text.enabled = false;
            yield return new WaitForSeconds(.4f);
            text.enabled = true;
            yield return new WaitForSeconds(.4f);
        }
    }
}
