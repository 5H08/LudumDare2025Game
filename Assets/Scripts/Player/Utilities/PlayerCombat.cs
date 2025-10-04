using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Coroutine punchRoutine = null;
    private GameObject trigger;

    void Start()
    {
        trigger = transform.GetChild(1).gameObject;
    }

    public void LeftPunch()
    {
        if (punchRoutine != null) 
        {
            StopCoroutine(punchRoutine);
            punchRoutine = StartCoroutine(PunchCoroutine());
        }
    }

    public void RightPunch()
    {
        if (punchRoutine != null)
        {
            StopCoroutine(punchRoutine);
            punchRoutine = StartCoroutine(PunchCoroutine());
        }
    }

    IEnumerator PunchCoroutine()
    {
        trigger.SetActive(false);
        yield return null;
        trigger.SetActive(true);
        yield return new WaitForSeconds(.5f);
        trigger.SetActive(false);
        punchRoutine = null;
    }
}
