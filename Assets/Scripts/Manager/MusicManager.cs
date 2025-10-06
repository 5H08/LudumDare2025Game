using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource musicSource;
    AudioClip attackMusic;
    AudioClip endMusic;
    bool attackStarted = false;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void StartAttack()
    {
        //if (!attackStarted)
        //{
        //    print("countdown");
        //    attackStarted = true;
        //    musicSource.Stop();
        //    musicSource.clip = attackMusic;
        //    musicSource.Play();
        //}
    }

    public void StartEndMusic()
    {
        //print("countdown");
        //musicSource.Stop();
        //musicSource.clip = endMusic;
        //musicSource.Play();
    }
}
