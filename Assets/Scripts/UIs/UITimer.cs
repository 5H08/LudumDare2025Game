using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    private TMP_Text timer;

    public int maxTime = 180;
    private float timeLeft;

    private void Start()
    {
        timeLeft = maxTime;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        int minutes = (int)(timeLeft / 60);
        int seconds = (int)(timeLeft % 60);
        string miliseconds = ((timeLeft % 60) - (int)(timeLeft % 60)).ToString("c2");
        timer.text = minutes + ":" + seconds + "." + miliseconds;
    }
}
