using System;
using System.Diagnostics;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgmSource; // 🎵 用于播放 BGM
    public AudioClip bgm1;        // 开始时播放
    public AudioClip bgm2;        // 第一次攻击后播放
    public AudioClip bgm3;        // 最后30秒倒计时播放

    private bool hasSwitchedToBGM2 = false; // ✅ 确保只切一次
    private bool hasSwitchedToBGM3 = false;
    private float totalTime = 300f;        
    private float timer;

    void Start()
    {
        timer = totalTime;
        // PlayBGM(bgm1);HERE
        UnityEngine.Debug.Log("bgm1");

    }



    void Update()
    {
        //  倒计时逻辑
        timer -= Time.deltaTime;

        // 🔁 检查倒计时剩余时间，30秒时切BGM3
        if (!hasSwitchedToBGM3 && timer <= 30f)
        {
            hasSwitchedToBGM3 = true;
            // PlayBGM(bgm3, true); HERE
            UnityEngine.Debug.Log("bgm3");
        }
    }

    // 🚨 外部调用：第一次攻击后调用这个函数
    public void OnFirstAttackTriggered()
    {
        if (!hasSwitchedToBGM2) // 只切换一次
        {
            hasSwitchedToBGM2 = true;
            //PlayBGM(bgm2, true);HERE
            UnityEngine.Debug.Log("bgm2");
        }
    }

    private void PlayBGM(AudioClip clip, bool loop = true)
    {
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }
}
