using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    private TMP_Text timer;
    private MainManager mainManager;

    bool calledEnd = false;
    public int maxTime = 180;
    private float timeLeft;

    private void Start()
    {
        timeLeft = maxTime;
        timer = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timer.text = "0:0.00";
            mainManager.EndRun("time");
        }

        if (timeLeft <= 60 && calledEnd)
        {
            calledEnd = true;
            mainManager.SixtySeconds();
        }

        else
        {
            int minutes = (int)(timeLeft / 60);
            int seconds = (int)(timeLeft % 60);
            string preSub = ((timeLeft % 60) - (int)(timeLeft % 60)).ToString("c2");
            string miliseconds = preSub.Substring(preSub.Length - 2);

            string secondsString = seconds.ToString();
            if (seconds < 10)
            {
                secondsString = "0" + seconds;
            }

            if (minutes == 0)
            {
                timer.text = secondsString + "." + miliseconds;
            }
            else
            {
                timer.text = minutes + ":" + secondsString + "." + miliseconds;
            }
        }


        
    }


}
