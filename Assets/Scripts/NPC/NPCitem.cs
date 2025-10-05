using UnityEngine;

public class NPCitem : MonoBehaviour
{
    [Header("掉落物 Prefab")]
    public GameObject itemPrefab;

    [Header("掉落偏移")]
    public Vector3 spawnOffset = new Vector3(0, 1f, 0);

    [Header("随机范围")]
    public float randomRadius = 0.5f; // 掉落的水平随机半径

    // 外部调用这个方法来生成掉落物
    public void SpawnItem(Vector3 position)
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("no itemPrefab");
            return;
        }

        // ✅ 在水平面上生成随机偏移（更自然的掉落感）
        Vector2 randomOffset = Random.insideUnitCircle * randomRadius;
        Vector3 spawnPos = position + spawnOffset + new Vector3(randomOffset.x, 0, randomOffset.y);

        Instantiate(itemPrefab, spawnPos, Quaternion.identity);
    }
}
