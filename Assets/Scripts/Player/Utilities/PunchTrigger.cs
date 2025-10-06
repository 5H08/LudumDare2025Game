using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    private Transform player;
    private bool cd = true;  


    void Start()
    {
        player = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "NPC")
        {
            print("Punched!");
            NPCBehavior combatScript = other.GetComponent<NPCBehavior>();
            if (combatScript != null)
            {
                combatScript.ReceiveHit(player);
            }
        }
    }
}
