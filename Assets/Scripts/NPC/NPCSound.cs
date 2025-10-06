using UnityEngine;

public class NPCSound : MonoBehaviour
{
    private AudioSource source;

    public AudioClip[] hitAudio;
    public AudioClip[] randomAudio;
    public AudioClip[] aggroAudio;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio(string type)
    {
        switch (type)
        {
            case "hit":
                source.clip = hitAudio[Random.Range(0, hitAudio.Length)];
                source.Play();
                break;

            case "random":
                source.clip = randomAudio[Random.Range(0, randomAudio.Length)];
                source.Play();
                break;

            case "aggro":
                source.clip = aggroAudio[Random.Range(0, aggroAudio.Length)];
                source.Play();
                break;
        }
    }
}
