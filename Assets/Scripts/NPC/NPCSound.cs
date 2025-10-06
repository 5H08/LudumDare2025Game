using UnityEngine;

public class NPCSound : MonoBehaviour
{
    private AudioSource source;

    public AudioClip[] firstHitAudio;
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
            case "first":
                source.PlayOneShot(firstHitAudio[Random.Range(0, firstHitAudio.Length)]);
                break;

            case "hit":
                source.PlayOneShot(hitAudio[Random.Range(0, hitAudio.Length)]);
                break;

            case "random":
                source.PlayOneShot(randomAudio[Random.Range(0, randomAudio.Length)]);
                break;

            case "aggro":
                source.PlayOneShot(aggroAudio[Random.Range(0, aggroAudio.Length)]);
                break;
        }
    }
}
