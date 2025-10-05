using UnityEngine;
using UnityEngine.AI;

public class PathVoiceTrigger : MonoBehaviour
{
    [Header("������������")]
    public float minInterval = 10f; // ��̼��
    public float maxInterval = 30f; // ����

    private NavMeshAgent agent;
    private float nextVoiceTime = 0f; // �´δ���ʱ��

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNextVoiceTime();
    }

    void Update()
    {
        if (agent == null) return;

        // ����Ƿ���Ѱ·
        bool isNavigating = agent.hasPath && agent.remainingDistance > agent.stoppingDistance;

        // �����Ѱ·������ʱ�䵽��
        if (isNavigating && Time.time >= nextVoiceTime)
        {
            TriggerVoice();      // ����������Ԥ��������
            SetNextVoiceTime();  // ������һ�δ���ʱ��
        }
    }

    void TriggerVoice()
    {
     
        Debug.Log("��������");
    }

    void SetNextVoiceTime()
    {
        float interval = Random.Range(minInterval, maxInterval);
        nextVoiceTime = Time.time + interval;
    }
}
