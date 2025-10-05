using UnityEngine;
using UnityEngine.AI;

public class NPCfindPlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("����")]
    public string playerTag = "Player";
    public float catchDistance = 1.5f;    // ����
    public float checkInterval = 0.1f;    // Ѱ·���

    private float checkTimer = 0f;

    // ׷������
    public static bool canChase = false;

    void Start()
    {
        //canChase = true;//�ǵ�ɾ
        agent = GetComponent<NavMeshAgent>();

        // �����
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;
        
        // �������û�򿪣���ֱ�Ӳ�׷
        if (!canChase)
        {
            agent.ResetPath();  // ֹͣ�ƶ�
            return;
        }

        // ˢ��·��
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            agent.SetDestination(player.position);
            checkTimer = 0f;
        }

        // �ж��Ƿ�ץ�����
        if (Vector3.Distance(transform.position, player.position) <= catchDistance)
        {
           // Debug.Log("udiedie");
            // TODO: ���������ʧ��ҳ���߼�
        }
    }
}
