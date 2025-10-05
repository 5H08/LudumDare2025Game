using UnityEngine;

public class itembehavior : MonoBehaviour
{
    [Header("旋转设置")]
    public Vector3 rotationSpeed = new Vector3(0, 50f, 0); // 每秒旋转角度

    [Header("漂浮设置")]
    public float floatAmplitude = 0.25f; // 漂浮高度幅度
    public float floatFrequency = 1f;    // 漂浮频率

    private Vector3 startPos;

    void Start()
    {
        // 记录初始位置
        startPos = transform.position;
    }

    void Update()
    {
        // 旋转物体
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);

        // 漂浮效果，使用正弦波上下浮动
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
