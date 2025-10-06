using System.Collections;
using UnityEngine;

public class NPCDamage : MonoBehaviour
{
    public int damage;
    public bool canDamage = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (canDamage)
            {
                PlayerCombat combatScript = other.gameObject.GetComponent<PlayerCombat>();
                if (combatScript != null)
                {
                    combatScript.ReduceHealth(damage);
                }
            }
            else
            {

            }
        }
    }
}
