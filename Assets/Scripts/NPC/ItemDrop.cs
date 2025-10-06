using System.Collections;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public float pickupCooldown = .75f;
    private bool canPickup = false;

    public Material icon;
    public string myName;
    public int myScore;

    public void SpawnSequence(Material mat, string itemName, int score)
    {
        icon = mat;
        myName = itemName;
        myScore = score;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = icon;
        StartCoroutine(cooldown());
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canPickup)
        {
            return;
        }
        if (other.tag == "Player")
        {
            MainManager manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MainManager>();
            manager.AdjustScore(myScore);
            manager.AddItem(myName);
            Destroy(gameObject);
        }
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(pickupCooldown);
        canPickup = true;
    }
}
