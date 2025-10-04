using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCcombat : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;

    [Header("击退")]
    public float knockbackForce = 3f;      // 水平距离
    public float knockbackHeight = 1f;     // 高度
    public float stunTime = 2f;            // 停行动时间
    public float knockbackCooldown = 0.5f;

    private bool isStunned = false;
    private bool canBeKnockedBack = true;

    // 追击开关
    public static bool canChase = false;
    private bool triggeredChase = false;

    [Header("追击")]
    public string playerTag = "Player";
    public float catchDistance = 1.5f;    // 抓到距离
    public float checkInterval = 0.1f;    // 刷新间隔

    private Transform player;
    private float checkTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        // 让 Rigidbody 不受 NavMeshAgent 的 root motion 干扰
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (player == null) return;

        if (!canChase)
        {
            agent.ResetPath();
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
            //Debug.Log("🎯 抓到玩家！");
            // TODO: 添加失败页面或扣血逻辑
        }
    }

    public void ReceiveHit(Transform attacker)
    {
        // 第一次被打后开启追击
        if (!triggeredChase)
        {
            canChase = true;
            triggeredChase = true;
        }

        if (!canBeKnockedBack) return;

        // 计算击退方向
        Vector3 dir = (transform.position - attacker.position).normalized;
        dir.y = 0.5f; // 向上击退
        dir.Normalize();

        // 暂时关闭导航
        if (agent != null)
            agent.enabled = false;

        // 清除原速度，防止击退叠加
        rb.linearVelocity = Vector3.zero;
        // 给一个斜上方的力
        rb.AddForce(dir * knockbackForce + Vector3.up * knockbackHeight, ForceMode.Impulse);

        StartCoroutine(KnockbackCooldownCoroutine());

        if (!isStunned)
            StartCoroutine(StunCoroutine());
    }

    private IEnumerator KnockbackCooldownCoroutine()
    {
        canBeKnockedBack = false;
        yield return new WaitForSeconds(knockbackCooldown);
        canBeKnockedBack = true;
    }

    private IEnumerator StunCoroutine()
    {
        isStunned = true;
        yield return new WaitForSeconds(stunTime);

        // 恢复寻路
        if (agent != null)
            agent.enabled = true;

        isStunned = false;
    }
}
