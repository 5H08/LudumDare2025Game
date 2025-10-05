using UnityEngine;

public class ItemAttract : MonoBehaviour
{
    [Header("吸附检测")]
    public string playerTag = "Player";   // 玩家tag
    public float detectRadius = 1.5f;     // 检测范围
    public float absorbSpeed = 10f;       // 吸附速度
    public float pickupDistance = 0.2f;   // 拾取距离

    [Header("千万不要动")]
    public float spawnBurstForce = 0f;    // 爆出力度
    public float upwardForce = 0f;        // 向上力度
    public float groundOffset = 0f;       // 离地高度
    public float attractDelay = 0.5f;     // 吸附延迟时间

    private Transform player;
    private bool absorbed = false;
    private Rigidbody rb;
    private Vector3 groundY;
    private float spawnTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnTime = Time.time;

        // 固定在地面上方一定高度
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f))
        {
            groundY = hit.point;
            transform.position = new Vector3(transform.position.x, groundY.y + groundOffset, transform.position.z);
        }

        // 添加初始爆出力
        if (rb != null)
        {
            Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0.3f, Random.Range(-1f, 1f)).normalized;
            rb.AddForce(randomDir * spawnBurstForce + Vector3.up * upwardForce, ForceMode.Impulse);
        }
    }

    void Update()
    {
        // 延迟吸附检测
        if (Time.time - spawnTime < attractDelay) return;

        // 如果正在吸附
        if (absorbed)
        {
            if (player != null)
            {
                // 吸向玩家身体中心（根据Collider自动居中）
                Vector3 targetPos = player.GetComponent<Collider>() ?
                    player.GetComponent<Collider>().bounds.center :
                    player.position + Vector3.up * 1f;

                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * absorbSpeed);

                if (Vector3.Distance(transform.position, targetPos) < pickupDistance)
                {
                    OnPickup();
                }
            }
            return;
        }

        // 检测附近是否有玩家进入范围
        Collider[] hits = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag(playerTag))
            {
                player = hit.transform;
                absorbed = true;

                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }
                break;
            }
        }
    }

    void OnPickup()
    {
        // 扩展
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
