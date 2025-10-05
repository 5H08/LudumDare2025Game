using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ���ܽŲ���ϵͳ
/// �Զ�����ɫ�Ƿ����ƶ����������ͣ���������Ӧ�Ų���Ч��
/// ��������ң�CharacterController����NPC��NavMeshAgent����
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SmartFootstepSound : MonoBehaviour
{
    [Header("ͨ������")]
    [Tooltip("�����NPC��ʹ��NavMeshAgentѰ·�����͹���")]
    public bool useNavMesh = false; // ���false��NPC true
    [Tooltip("�Ų�֮��Ļ���ʱ�������룩")]
    public float baseStepInterval = 0.5f;
    [Tooltip("��С�ٶȣ�С������ٶȲ��Ქ�ŽŲ�����")]
    public float minSpeedForStep = 0.1f;
    [Tooltip("�Ų�����")]
    public float volume = 0.7f;
    [Tooltip("���ڼ���������߾���")]
    public float raycastDistance = 1.2f;

    [Header("���������Ч")]
    [Tooltip("�ݵؽŲ���Ч��")]
    public AudioClip[] grassClips;
    [Tooltip("ʯͷ�Ų���Ч��")]
    public AudioClip[] stoneClips;
    [Tooltip("ľ��Ų���Ч��")]
    public AudioClip[] woodClips;
    [Tooltip("Ĭ����Ч�أ����û��⵽����Tag��")]
    public AudioClip[] defaultClips;

    // ˽�б���
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private CharacterController controller;
    private float nextStepTime = 0f;

    void Start()
    {
        // �Զ���ȡAudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // �ռ仯������NPC���о���˥����

        // ����useNavMesh����ʹ���ĸ����
        if (useNavMesh)
            agent = GetComponent<NavMeshAgent>();
        else
            controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float speed = 0f;

        // ���ݿ������ͻ�ȡ�ٶ�
        if (useNavMesh && agent != null)
            speed = agent.velocity.magnitude;
        else if (controller != null)
            speed = controller.velocity.magnitude;

        // ����ٶȳ�����ֵ�ҵ��ﲥ�ż�����򲥷ŽŲ���
        if (speed > minSpeedForStep && Time.time >= nextStepTime)
        {
            PlayFootstep();
            nextStepTime = Time.time + GetStepInterval(speed);
        }
    }

    /// <summary>
    /// ���Ŷ�Ӧ������ʵĽŲ���
    /// </summary>
    void PlayFootstep()
    {
        AudioClip[] clipPool = GetClipsBasedOnGround();
        if (clipPool.Length == 0) return;

        AudioClip clip = clipPool[Random.Range(0, clipPool.Length)];
        audioSource.pitch = Random.Range(0.9f, 1.1f); // �����������������Ȼ
        audioSource.PlayOneShot(clip, volume);
    }

    /// <summary>
    /// ���ݵ���Tag���ض�Ӧ�ĽŲ���Ч��
    /// </summary>
    AudioClip[] GetClipsBasedOnGround()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.3f, Vector3.down, out RaycastHit hit, raycastDistance))
        {
            string tag = hit.collider.tag;

            switch (tag)
            {
                case "Grass":
                    return grassClips;
                case "Stone":
                    return stoneClips;
                case "Wood":
                    return woodClips;
            }
        }

        return defaultClips; // û��⵽Tagʱ����Ĭ��
    }

    /// <summary>
    /// �����ٶȵ����Ų������Խ����Խ��
    /// </summary>
    float GetStepInterval(float speed)
    {
        float interval = baseStepInterval / Mathf.Clamp(speed, 0.5f, 5f);
        return Mathf.Clamp(interval, 0.2f, 0.7f);
    }
}
//��û�ӵ�npc������ϣ��ǵü�.
//��ң�Player��

//�����������ϴ˽ű���

//ȷ������� CharacterController��

//ȡ����ѡ useNavMesh��

//�����Ӧ�ĽŲ���Ч������ grassClips��stoneClips �ȣ���

//���� baseStepInterval ���ƽŲ�Ƶ�ʡ�
//-----------------------------------------
//NPC

//��NPC���ϴ˽ű���

//ȷ��NPC�� NavMeshAgent��

//��ѡ useNavMesh��

//ͬ��������Ч��

//�ű������NPCѰ·�ٶ��Զ����ŽŲ�����