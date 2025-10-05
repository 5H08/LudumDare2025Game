using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    double currentTime;
    public float startingTime = 10f; 

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = 0;
            // 结束后事件
        }

        // 转换
        int minutes = (int)(currentTime / 60);
        double seconds = currentTime % 60;

       
        countdownText.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }
}
