using UnityEngine;

public class NPCDamage : MonoBehaviour
{
    public int damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerCombat combatScript = other.gameObject.GetComponent<PlayerCombat>();
            if (combatScript != null )
            {
                combatScript.ReduceHealth(damage);
            }
        }
    }
}
