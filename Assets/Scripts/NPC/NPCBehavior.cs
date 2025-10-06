using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class NPCBehavior : MonoBehaviour
{
    private NavMeshAgent agent; // Unity ����ϵͳ��������ڿ��� NPC �Զ�Ѱ·
    private Rigidbody rb;
    private Animator animator;

    private NPCSound sound;
    private NPCDamage damage;
    private MainManager manager;
    private MusicManager music;

    [Header("����")]
    public float knockBackMagnitude = 50f;      // ����ʱˮƽ�ƶ��ľ��루�Ӿ����൱�ڡ�����ɡ���Զ��
    public float knockbackHeight = .75f;     // ����ʱ�����ĸ߶ȣ���������ķ��ȣ�
    public float knockbackDuration = 1f; // ���˶�������ʱ�䣨Lerp ��ֵ����ʱ�䣩
    public float stunTime = 2f;            // �����к�ʧȥ�ж�������ʱ��
    public float stillThreshold = .5f;
    private bool knockbackRunning = false;
    public int firstHitScore = 20;
    public int hitScore = 5;
    private Coroutine currentKnockbackCoroutine = null;

    // === ׷������ ===
    private bool triggeredChase = false;
    public static bool canChase = false;   // �Ƿ����� NPC ׷����ң�ȫ�־�̬������
    private Coroutine currentChaseCoroutine = null;

    [Header("Buffing")]
    public float baseSpeed = 3.5f;
    public float speedIncrement = .5f;

    [Header("׷��")]
    private Transform player;              // ���λ������
    public string playerTag = "Player";    // �������� Tag ����

    public float checkInterval = 0.1f;     // ���ˢ��һ�ε���·��

    [Header("Item")]
    public Material itemIcon;
    public int itemScore;
    public string itemName;

    void Start()
    {
        // ��ȡ���
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        damage = transform.GetChild(1).GetComponent<NPCDamage>();
        sound = GetComponent<NPCSound>();
        music = GameObject.FindGameObjectWithTag("Manager").GetComponent<MusicManager>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MainManager>();

        agent.speed = baseSpeed;

        // Ѱ����Ҷ���
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

    // Ѱ·Я��
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
        //����д�ܻ���Ч
        // Activates chase upon first hit
        manager.AdjustScore(hitScore);

        damage.canDamage = false;
        
        if (!triggeredChase)
        {
            manager.AdjustScore(firstHitScore);
            triggeredChase = true;
            animator.SetBool("StartedRunning", true);
            sound.PlayAudio("first");
            StartCoroutine(AggroVoiceLoop());

            NPCitem itemComp = GetComponent<NPCitem>();
            if (itemComp != null)
            {
                itemComp.SpawnItem(transform.position);
                music.StartAttack();
            }
            else
            {
                print("Passive NPC!");
                music.StartAttack();
            }
        }
        else
        {
            if (triggeredChase)
            {
                sound.PlayAudio("hit");
            }
            agent.speed += speedIncrement;
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

        // ��������Э�̣����ƻ��˶�����
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

    IEnumerator AggroVoiceLoop()
    {
        yield return new WaitForSeconds(Random.Range(5f, 15f));
        sound.PlayAudio("aggro");
        StartCoroutine(AggroVoiceLoop());
    }

    private IEnumerator ApplyKnockback(Vector3 force)
    {
        animator.SetBool("Hit", true);
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

        animator.SetBool("Hit", false);
        damage.canDamage = true;
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
        animator.SetBool("Hit", true);
        rb.AddForce(force * knockBackMagnitude, ForceMode.Impulse);
        yield return new WaitForFixedUpdate();
        float knockbackTime = Time.time;
        yield return new WaitUntil(
                                    () => rb.linearVelocity.magnitude < stillThreshold ||
                                    Time.time > knockbackTime + knockbackDuration
                                   );
        yield return new WaitForSeconds(stunTime);

        animator.SetBool("Hit", false);
        damage.canDamage = true;
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
