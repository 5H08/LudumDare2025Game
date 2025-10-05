using UnityEngine;
using UnityEngine.AI;

public class NPCfindPlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("设置")]
    public string playerTag = "Player";
    public float catchDistance = 1.5f;    // 距离
    public float checkInterval = 0.1f;    // 寻路间隔

    private float checkTimer = 0f;

    // 追击开关
    public static bool canChase = false;

    void Start()
    {
        //canChase = true;//记得删
        agent = GetComponent<NavMeshAgent>();

        // 找玩家
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;
        
        // 如果开关没打开，就直接不追
        if (!canChase)
        {
            agent.ResetPath();  // 停止移动
            return;
        }

        // 刷新路径
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            agent.SetDestination(player.position);
            checkTimer = 0f;
        }

        // 判断是否抓到玩家
        if (Vector3.Distance(transform.position, player.position) <= catchDistance)
        {
           // Debug.Log("udiedie");
            // TODO: 在这里加上失败页面逻辑
        }
    }
}
