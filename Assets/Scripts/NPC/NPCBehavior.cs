using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class NPCBehavior : MonoBehaviour
{
    private NavMeshAgent agent; // Unity 导航系统组件，用于控制 NPC 自动寻路
    private Rigidbody rb;

    [Header("击退")]
    public float knockBackMagnitude = 50f;      // 击退时水平移动的距离（视觉上相当于“被打飞”多远）
    public float knockbackHeight = .75f;     // 击退时上升的高度（向上抛起的幅度）
    public float knockbackDuration = 1f; // 击退动画持续时间（Lerp 插值过渡时间）
    public float stunTime = 2f;            // 被击中后失去行动能力的时间
    public float stillThreshold = .5f;
    private bool knockbackRunning = false;
    private Coroutine currentKnockbackCoroutine = null;

    // === 追击控制 ===
    private bool triggeredChase = false;
    public static bool canChase = false;   // 是否允许 NPC 追击玩家（全局静态变量）
    private Coroutine currentChaseCoroutine = null;


    [Header("追击")]
    private Transform player;              // 玩家位置引用
    public string playerTag = "Player";    // 玩家物体的 Tag 名称

    public float checkInterval = 0.1f;     // 多久刷新一次导航路径

    void Start()
    {
        // 获取组件
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        // 寻找玩家对象
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.Log("NPC: Can'f find player!");
        }
    }

    void Update()
    {

    }

    // 寻路携程
    private IEnumerator NavCycle()
    {
        yield return null;
        while (true)
        {
            agent.SetDestination(player.position);
            yield return new WaitForSeconds(checkInterval);
        }
    }

    // Called by trigger script when attacked
    public void ReceiveHit(Transform attacker)
    {
        // Activates chase upon first hit
        if (!triggeredChase)
        {
            triggeredChase = true;
        }
        else
        {
            if (currentChaseCoroutine != null && agent.enabled == true)
            {
                StopCoroutine(currentChaseCoroutine);
                agent.ResetPath();
            }
        }

        // Calculate force
        Vector3 dir = (transform.position - attacker.position);
        dir.Normalize();
        dir.y += knockbackHeight;
        dir.Normalize();

        // 启动击退协程（控制击退动画）
        if (knockbackRunning)
        {
            StopCoroutine(currentKnockbackCoroutine);
            currentKnockbackCoroutine = StartCoroutine(ChainedKnockback(dir));
        }
        else
        {
            currentKnockbackCoroutine = StartCoroutine(ApplyKnockback(dir));
        }
    }

    private IEnumerator ApplyKnockback(Vector3 force)
    {
        // Enable rb and disable agent
        knockbackRunning = true;
        yield return null;
        agent.enabled = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        yield return null;

        // Add force for knockback. Resumes nav after stopping / enough time passed + stuntime.
        rb.AddForce(force * knockBackMagnitude, ForceMode.Impulse);
        yield return new WaitForFixedUpdate();
        float knockbackTime = Time.time;
        yield return new WaitUntil(
                                    () => rb.linearVelocity.magnitude < stillThreshold ||
                                    Time.time > knockbackTime + knockbackDuration
                                   );
        yield return new WaitForSeconds(stunTime);

        knockbackRunning = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        agent.Warp(transform.position);
        agent.enabled = true;
        currentChaseCoroutine = StartCoroutine(NavCycle());
        yield return null;
    }

    private IEnumerator ChainedKnockback(Vector3 force)
    {
        rb.AddForce(force * knockBackMagnitude, ForceMode.Impulse);
        yield return new WaitForFixedUpdate();
        float knockbackTime = Time.time;
        yield return new WaitUntil(
                                    () => rb.linearVelocity.magnitude < stillThreshold ||
                                    Time.time > knockbackTime + knockbackDuration
                                   );
        yield return new WaitForSeconds(stunTime);

        knockbackRunning = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        agent.Warp(transform.position);
        agent.enabled = true;
        currentChaseCoroutine = StartCoroutine(NavCycle());
        yield return null;
    }
}
