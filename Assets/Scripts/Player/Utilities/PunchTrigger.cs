using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    private Transform player;
    private bool cd = true;  
    private float cooldown = 5f;   
    private float lastPunchTime = -999f; 

    void Start()
    {
        player = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!cd && Time.time - lastPunchTime < cooldown)
            return;

        if (other.gameObject.tag == "NPC")
        {
            print("Punched!");
            NPCBehavior combatScript = other.GetComponent<NPCBehavior>();
            if (combatScript != null)
            {
                combatScript.ReceiveHit(player);
            }

           
            lastPunchTime = Time.time;
            cd = false;
            Invoke(nameof(ResetCooldown), cooldown);
        }
    }

    private void ResetCooldown()
    {
        cd = true;
    }
}
