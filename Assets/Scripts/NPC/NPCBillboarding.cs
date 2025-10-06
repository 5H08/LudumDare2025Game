using UnityEngine;

public class NPCBillboarding : MonoBehaviour
{
    public string playerTag = "Player";
    [SerializeField] private Transform player;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(player);
    }
}
