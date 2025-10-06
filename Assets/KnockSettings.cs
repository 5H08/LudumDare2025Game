using System.Collections;
using UnityEngine;

public class KnockSettings : MonoBehaviour
{
    AudioSource source;
    AudioSource sorryer;
    public AudioClip sorry;
    public AudioClip original;

    bool knocked = false;
    bool saySorry = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        sorryer = transform.GetChild(0).GetComponent<AudioSource>();
        original = source.clip;
    }

    public void PlayKnock()
    {
        if (!knocked)
        {
            knocked = true;
            StartCoroutine(WaitSorry());
        }
        if (saySorry)
        {
            print("sorry'd");
            sorryer.Play();
            saySorry=false;
        }
        else
        {
            source.clip=original;
            source.Play();
        }
    }

    IEnumerator WaitSorry()
    {
        yield return new WaitForSeconds(3.5f);
        saySorry = true;
    }

}
