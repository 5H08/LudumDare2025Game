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
        countdownText.text = currentTime.ToString("F2");

        if (currentTime <= 0)
        {
            currentTime = 0;
            // 结束后事件
        }
    }
}

