using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCcombat : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    public float knockbackForce = 5f;  // »÷ÍË¾àÀë
    public float stunTime = 2f;        // Ê±¼ä
    public float knockbackCooldown = 0.5f; 

    private bool isStunned = false;
    private bool canBeKnockedBack = true; // ¿ØÖÆ»÷ÍËÀäÈ´

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    public void ReceiveHit(Transform player)
    {
        Debug.Log("iii");
        if (!canBeKnockedBack) return;

        Vector3 dir = (transform.position - player.position).normalized;

    
        if (agent != null)
            agent.isStopped = true;

 
        if (rb != null)
            rb.AddForce(dir * knockbackForce, ForceMode.Impulse);

     
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
