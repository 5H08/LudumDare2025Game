using UnityEngine;
using UnityEngine.AI;

public class PathVoiceTrigger : MonoBehaviour
{
    [Header("语音触发设置")]
    public float minInterval = 10f; // 最短间隔
    public float maxInterval = 30f; // 最长间隔

    private NavMeshAgent agent;
    private float nextVoiceTime = 0f; // 下次触发时间

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNextVoiceTime();
    }

    void Update()
    {
        if (agent == null) return;

        // 检测是否在寻路
        bool isNavigating = agent.hasPath && agent.remainingDistance > agent.stoppingDistance;

        // 如果在寻路，并且时间到了
        if (isNavigating && Time.time >= nextVoiceTime)
        {
            TriggerVoice();      // 触发语音（预留函数）
            SetNextVoiceTime();  // 设置下一次触发时间
        }
    }

    void TriggerVoice()
    {
     
        Debug.Log("触发语音");
    }

    void SetNextVoiceTime()
    {
        float interval = Random.Range(minInterval, maxInterval);
        nextVoiceTime = Time.time + interval;
    }
}
