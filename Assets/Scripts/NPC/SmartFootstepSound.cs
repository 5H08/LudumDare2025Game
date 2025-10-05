using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 智能脚步声系统
/// 自动检测角色是否在移动、地面类型，并播放相应脚步音效。
/// 可用于玩家（CharacterController）和NPC（NavMeshAgent）。
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SmartFootstepSound : MonoBehaviour
{
    [Header("通用设置")]
    [Tooltip("如果是NPC（使用NavMeshAgent寻路），就勾上")]
    public bool useNavMesh = false; // 玩家false，NPC true
    [Tooltip("脚步之间的基础时间间隔（秒）")]
    public float baseStepInterval = 0.5f;
    [Tooltip("最小速度（小于这个速度不会播放脚步声）")]
    public float minSpeedForStep = 0.1f;
    [Tooltip("脚步音量")]
    public float volume = 0.7f;
    [Tooltip("用于检测地面的射线距离")]
    public float raycastDistance = 1.2f;

    [Header("地面材质音效")]
    [Tooltip("草地脚步音效池")]
    public AudioClip[] grassClips;
    [Tooltip("石头脚步音效池")]
    public AudioClip[] stoneClips;
    [Tooltip("木板脚步音效池")]
    public AudioClip[] woodClips;
    [Tooltip("默认音效池（如果没检测到地面Tag）")]
    public AudioClip[] defaultClips;

    // 私有变量
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private CharacterController controller;
    private float nextStepTime = 0f;

    void Start()
    {
        // 自动获取AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // 空间化声音（NPC会有距离衰减）

        // 根据useNavMesh决定使用哪个组件
        if (useNavMesh)
            agent = GetComponent<NavMeshAgent>();
        else
            controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float speed = 0f;

        // 根据控制类型获取速度
        if (useNavMesh && agent != null)
            speed = agent.velocity.magnitude;
        else if (controller != null)
            speed = controller.velocity.magnitude;

        // 如果速度超过阈值且到达播放间隔，则播放脚步音
        if (speed > minSpeedForStep && Time.time >= nextStepTime)
        {
            PlayFootstep();
            nextStepTime = Time.time + GetStepInterval(speed);
        }
    }

    /// <summary>
    /// 播放对应地面材质的脚步声
    /// </summary>
    void PlayFootstep()
    {
        AudioClip[] clipPool = GetClipsBasedOnGround();
        if (clipPool.Length == 0) return;

        AudioClip clip = clipPool[Random.Range(0, clipPool.Length)];
        audioSource.pitch = Random.Range(0.9f, 1.1f); // 随机音调让声音更自然
        audioSource.PlayOneShot(clip, volume);
    }

    /// <summary>
    /// 根据地面Tag返回对应的脚步音效池
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

        return defaultClips; // 没检测到Tag时返回默认
    }

    /// <summary>
    /// 根据速度调整脚步间隔，越快间隔越短
    /// </summary>
    float GetStepInterval(float speed)
    {
        float interval = baseStepInterval / Mathf.Clamp(speed, 0.5f, 5f);
        return Mathf.Clamp(interval, 0.2f, 0.7f);
    }
}
//还没加到npc和玩家上，记得加.
//玩家（Player）

//给玩家物体挂上此脚本；

//确保玩家有 CharacterController；

//取消勾选 useNavMesh；

//拖入对应的脚步音效（例如 grassClips、stoneClips 等）；

//调整 baseStepInterval 控制脚步频率。
//-----------------------------------------
//NPC

//给NPC挂上此脚本；

//确保NPC有 NavMeshAgent；

//勾选 useNavMesh；

//同样拖入音效；

//脚本会根据NPC寻路速度自动播放脚步声。