using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCcombat : MonoBehaviour
{
    private NavMeshAgent agent; // Unity 导航系统组件，用于控制 NPC 自动寻路

    [Header("击退")]
    public float knockbackForce = 3f;      // 击退时水平移动的距离（视觉上相当于“被打飞”多远）
    public float knockbackHeight = 1f;     // 击退时上升的高度（向上抛起的幅度）
    public float knockbackDuration = 0.2f; // 击退动画持续时间（Lerp 插值过渡时间）
    public float stunTime = 2f;            // 被击中后失去行动能力的时间
    public float knockbackCooldown = 0.5f; // 两次击退之间的冷却时间，避免重复触发

    private bool isStunned = false;        // 是否处于“眩晕”状态（不能行动）
    private bool canBeKnockedBack = true;  // 是否可以被击退（防止短时间多次击退）

    // === 追击控制 ===
    public static bool canChase = false;   // 是否允许 NPC 追击玩家（全局静态变量）
    private bool triggeredChase = false;   // 是否已经被玩家打过（打过一次后才开始追击）

    [Header("追击")]
    public string playerTag = "Player";    // 玩家物体的 Tag 名称
    public float catchDistance = 1.5f;     // 当与玩家距离小于此值时视为“抓到玩家”
    public float checkInterval = 0.1f;     // 多久刷新一次导航路径

    private Transform player;              // 玩家位置引用
    private float checkTimer = 0f;         // 计时器，用于控制路径刷新间隔

    void Start()
    {
        // 获取导航组件
        agent = GetComponent<NavMeshAgent>();

        // 寻找玩家对象
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        // 如果找不到玩家对象，则直接返回
        if (player == null) return;

        // 如果当前不允许追击（例如还没被打）
        if (!canChase)
        {
            agent.ResetPath(); // 停止寻路
            return;
        }

        // === 定时刷新路径 ===
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            // 更新目标位置为玩家位置
            agent.SetDestination(player.position);
            checkTimer = 0f;
        }

        // === 检查是否“抓到玩家” ===
        if (Vector3.Distance(transform.position, player.position) <= catchDistance)
        {
            // Debug.Log("🎯 抓到玩家！");
            // TODO: 在这里添加玩家被抓逻辑
        }
    }

    // ===============================
    // 被玩家攻击时调用（从外部脚本触发）
    // ===============================
    public void ReceiveHit(Transform attacker)
    {
        // 第一次被打中才会开启追击模式
        if (!triggeredChase)
        {
            canChase = true;
            triggeredChase = true;
        }

        // 如果在冷却中则不触发击退
        if (!canBeKnockedBack) return;

        // === 计算击退方向 ===
        Vector3 dir = (transform.position - attacker.position).normalized; // NPC -> 攻击者 的方向反向
        dir.y = 0.5f; // 稍微向上，让击退有一点“飞起”感觉
        dir.Normalize();

        // 暂停导航系统（防止击退时被导航系统强制拉回原路径）
        if (agent != null)
            agent.isStopped = true;

        // 启动击退协程（控制击退动画）
        StartCoroutine(KnockbackCoroutine(dir));

        // 启动冷却协程（防止短时间多次触发）
        StartCoroutine(KnockbackCooldownCoroutine());

        // 如果当前不在眩晕状态，则启动眩晕计时
        if (!isStunned)
            StartCoroutine(StunCoroutine());
    }

    // ===============================
    // 击退动画协程（通过 Lerp 平滑移动）
    // ===============================
    private IEnumerator KnockbackCoroutine(Vector3 dir)
    {
        Vector3 startPos = transform.position;          // 起点
        Vector3 endPos = startPos + dir * knockbackForce; // 水平击退距离
        endPos.y = startPos.y + knockbackHeight;        // 加上向上偏移（抬高）

        float timer = 0f;
        while (timer < knockbackDuration)
        {
            // 插值移动（线性平滑过渡）
            transform.position = Vector3.Lerp(startPos, endPos, timer / knockbackDuration);
            timer += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 最终位置修正
        transform.position = endPos;
    }

    // ===============================
    // 击退冷却协程（防止频繁击退）
    // ===============================
    private IEnumerator KnockbackCooldownCoroutine()
    {
        canBeKnockedBack = false;               // 临时关闭击退
        yield return new WaitForSeconds(knockbackCooldown); // 等待冷却结束
        canBeKnockedBack = true;                // 恢复可击退
    }

    // ===============================
    // 眩晕状态协程（被打后停止行动）
    // ===============================
    private IEnumerator StunCoroutine()
    {
        isStunned = true; // 标记为“晕眩中”
        yield return new WaitForSeconds(stunTime); // 等待指定时间

        // 眩晕结束，恢复导航
        if (agent != null)
            agent.isStopped = false;

        isStunned = false; // 恢复正常
    }
}
