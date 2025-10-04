using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCcombat : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("击退")]
    public float knockbackForce = 3f;      // 水平距离
    public float knockbackHeight = 1f;     // 高度
    public float knockbackDuration = 0.2f; // 时间
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
       
        if (!triggeredChase)
        {
            canChase = true;
            triggeredChase = true;
        }

        if (!canBeKnockedBack) return;

       
        Vector3 dir = (transform.position - attacker.position).normalized;
        dir.y = 0.5f; // 向上
        dir.Normalize();

        if (agent != null)
            agent.isStopped = true;

        StartCoroutine(KnockbackCoroutine(dir));
        StartCoroutine(KnockbackCooldownCoroutine());

        if (!isStunned)
            StartCoroutine(StunCoroutine());
    }

    private IEnumerator KnockbackCoroutine(Vector3 dir)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + dir * knockbackForce;
        endPos.y = startPos.y + knockbackHeight; // 锁地面

        float timer = 0f;
        while (timer < knockbackDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, timer / knockbackDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
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

        if (agent != null)
            agent.isStopped = false;

        isStunned = false;
    }
}
