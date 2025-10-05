using UnityEngine;

public class itembehavior : MonoBehaviour
{
    [Header("��ת����")]
    public Vector3 rotationSpeed = new Vector3(0, 50f, 0); // ÿ����ת�Ƕ�

    [Header("Ư������")]
    public float floatAmplitude = 0.25f; // Ư���߶ȷ���
    public float floatFrequency = 1f;    // Ư��Ƶ��

    private Vector3 startPos;

    void Start()
    {
        // ��¼��ʼλ��
        startPos = transform.position;
    }

    void Update()
    {
        // ��ת����
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);

        // Ư��Ч����ʹ�����Ҳ����¸���
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
