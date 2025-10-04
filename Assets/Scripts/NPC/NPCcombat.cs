using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCcombat : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    public float knockbackForce = 3f;  // 击退距离
    public float stunTime = 2f;        // 时间
    public float knockbackCooldown = 0.5f; 

    private bool isStunned = false;
    private bool canBeKnockedBack = true; // 控制击退冷却

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    public void ReceiveHit(Transform player)
    {
        if (!canBeKnockedBack) return;

      
        Vector3 dir = (transform.position - player.position).normalized;

       
        dir.y = 1f;  // 上击退
        dir.Normalize(); // 保持方向向量

        if (agent != null)
            agent.isStopped = true;

      
        Vector3 knockback = dir * knockbackForce;
        transform.position += knockback;

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

        if (agent != null)
            agent.isStopped = false;

        isStunned = false;
    }
}
