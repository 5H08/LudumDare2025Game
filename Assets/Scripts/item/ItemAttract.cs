using UnityEngine;

public class ItemAttract : MonoBehaviour
{
    [Header("�������")]
    public string playerTag = "Player";   // ���tag
    public float detectRadius = 1.5f;     // ��ⷶΧ
    public float absorbSpeed = 10f;       // �����ٶ�
    public float pickupDistance = 0.2f;   // ʰȡ����

    [Header("ǧ��Ҫ��")]
    public float spawnBurstForce = 0f;    // ��������
    public float upwardForce = 0f;        // ��������
    public float groundOffset = 0f;       // ��ظ߶�
    public float attractDelay = 0.5f;     // �����ӳ�ʱ��

    private Transform player;
    private bool absorbed = false;
    private Rigidbody rb;
    private Vector3 groundY;
    private float spawnTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnTime = Time.time;

        // �̶��ڵ����Ϸ�һ���߶�
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f))
        {
            groundY = hit.point;
            transform.position = new Vector3(transform.position.x, groundY.y + groundOffset, transform.position.z);
        }

        // ��ӳ�ʼ������
        if (rb != null)
        {
            Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0.3f, Random.Range(-1f, 1f)).normalized;
            rb.AddForce(randomDir * spawnBurstForce + Vector3.up * upwardForce, ForceMode.Impulse);
        }
    }

    void Update()
    {
        // �ӳ��������
        if (Time.time - spawnTime < attractDelay) return;

        // �����������
        if (absorbed)
        {
            if (player != null)
            {
                // ��������������ģ�����Collider�Զ����У�
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

        // ��⸽���Ƿ�����ҽ��뷶Χ
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
        // ��չ
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
