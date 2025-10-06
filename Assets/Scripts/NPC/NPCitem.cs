using UnityEngine;

public class NPCitem : MonoBehaviour
{
    [Header("掉落物 Prefab")]
    public GameObject itemPrefab;
    private NPCBehavior behavior;

    private void Start()
    {
        behavior = GetComponent<NPCBehavior>(); 
    }

    // 外部调用这个方法来生成掉落物
    public void SpawnItem(Vector3 position)
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("no itemPrefab");
            return;
        }

        GameObject myItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        ItemDrop itemScript = myItem.GetComponent<ItemDrop>();
        if (itemScript != null)
        {
            itemScript.SpawnSequence(behavior.itemIcon, behavior.itemName, behavior.itemScore);
        }
    }
}
