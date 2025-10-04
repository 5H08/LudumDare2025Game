using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            print("Punched!");
            NPCcombat combatScript = other.GetComponent<NPCcombat>();
            if (combatScript != null)
            {
                combatScript.ReceiveHit(player);
            }
        }
    }
}
