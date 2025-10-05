using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    [Header("��ת����")]
    public Vector3 rotationSpeed = new Vector3(0, 50f, 0); // ÿ����ת�Ƕ�

    [Header("Ư������")]
    public float floatAmplitude = 0.20f; // Ư���߶ȷ���
    public float floatFrequency = 1f;    // Ư��Ƶ��
    public float floatHeight = 1f;       // ��غ�Ư���߶�

    [Header("���е���������")]
    public float arcHeight = 2f;         // ���л��߸߶�
    public float fallDuration = 1f;      // ����ʱ��
    private bool landed = false;         // �Ƿ����

    [Header("��������")]
    public float explosionRadius = 1f;   // ����ƫ�ư뾶
    public float explosionUpward = 1f;   // �������Ϸ���

    private Vector3 startPos;
    private Vector3 targetPos;
    private float spawnTime;

    void Start()
    {
        // �����������ƫ��
        Vector3 randomOffset = new Vector3(
            Random.Range(-explosionRadius, explosionRadius),
            Random.Range(0, explosionUpward),
            Random.Range(-explosionRadius, explosionRadius)
        );

        startPos = transform.position + randomOffset;

        // ���߼�����
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
            targetPos = hit.point + Vector3.up * floatHeight; // �������΢Ư��
        }
        else
        {
            targetPos = transform.position; // û��⵽����Ͳ��ƶ�
            landed = true;
        }

        spawnTime = Time.time;
    }

    void Update()
    {
        // �����û��أ������߷���
        if (!landed)
        {
            float t = (Time.time - spawnTime) / fallDuration;
            if (t >= 1f)
            {
                t = 1f;
                landed = true;
            }

            // ���߲�ֵ
            Vector3 midPoint = (startPos + targetPos) / 2 + Vector3.up * arcHeight;
            Vector3 m1 = Vector3.Lerp(startPos, midPoint, t);
            Vector3 m2 = Vector3.Lerp(midPoint, targetPos, t);
            transform.position = Vector3.Lerp(m1, m2, t);
        }
        else
        {
            // ��غ���ת + Ư��Ч��
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);

            float newY = targetPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
