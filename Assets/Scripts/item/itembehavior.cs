using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    [Header("旋转设置")]
    public Vector3 rotationSpeed = new Vector3(0, 50f, 0); // 每秒旋转角度

    [Header("漂浮设置")]
    public float floatAmplitude = 0.20f; // 漂浮高度幅度
    public float floatFrequency = 1f;    // 漂浮频率
    public float floatHeight = 1f;       // 落地后漂浮高度

    [Header("飞行到地面设置")]
    public float arcHeight = 2f;         // 飞行弧线高度
    public float fallDuration = 1f;      // 飞行时间
    private bool landed = false;         // 是否落地

    [Header("爆出设置")]
    public float explosionRadius = 1f;   // 爆出偏移半径
    public float explosionUpward = 1f;   // 爆出向上幅度

    private Vector3 startPos;
    private Vector3 targetPos;
    private float spawnTime;

    void Start()
    {
        // 爆出方向随机偏移
        Vector3 randomOffset = new Vector3(
            Random.Range(-explosionRadius, explosionRadius),
            Random.Range(0, explosionUpward),
            Random.Range(-explosionRadius, explosionRadius)
        );

        startPos = transform.position + randomOffset;

        // 射线检测地面
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
            targetPos = hit.point + Vector3.up * floatHeight; // 离地面略微漂浮
        }
        else
        {
            targetPos = transform.position; // 没检测到地面就不移动
            landed = true;
        }

        spawnTime = Time.time;
    }

    void Update()
    {
        // 如果还没落地，做弧线飞行
        if (!landed)
        {
            float t = (Time.time - spawnTime) / fallDuration;
            if (t >= 1f)
            {
                t = 1f;
                landed = true;
            }

            // 弧线插值
            Vector3 midPoint = (startPos + targetPos) / 2 + Vector3.up * arcHeight;
            Vector3 m1 = Vector3.Lerp(startPos, midPoint, t);
            Vector3 m2 = Vector3.Lerp(midPoint, targetPos, t);
            transform.position = Vector3.Lerp(m1, m2, t);
        }
        else
        {
            // 落地后旋转 + 漂浮效果
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);

            float newY = targetPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
