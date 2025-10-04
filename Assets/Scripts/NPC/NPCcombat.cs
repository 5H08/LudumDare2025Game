using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCcombat : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    public float knockbackForce = 3f;  // ���˾���
    public float stunTime = 2f;        // ʱ��
    public float knockbackCooldown = 0.5f; 

    private bool isStunned = false;
    private bool canBeKnockedBack = true; // ���ƻ�����ȴ

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    public void ReceiveHit(Transform player)
    {
        if (!canBeKnockedBack) return;

      
        Vector3 dir = (transform.position - player.position).normalized;

       
        dir.y = 1f;  // �ϻ���
        dir.Normalize(); // ���ַ�������

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
