using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private Animator leftAnim;
    [SerializeField] private Animator rightAnim;

    private Coroutine punchRoutine = null;
    private bool canLeft = true;
    private bool canRight = true;

    void Start()
    {
        trigger = transform.GetChild(1).gameObject;
        GameObject canv = GameObject.FindGameObjectWithTag("UI Canvas");
        leftAnim = canv.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        rightAnim = canv.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
    }

    public void LeftPunch()
    {
        if (canLeft)
        {
            StartCoroutine(LeftCooldown());
            leftAnim.SetBool("Punching", true);
            if (punchRoutine != null)
            {
                StopCoroutine(punchRoutine);
                punchRoutine = StartCoroutine(PunchCoroutine());
            }
            else
            {
                punchRoutine = StartCoroutine(PunchCoroutine());
            }
        }
        else
        {

        }
    }

    public void RightPunch()
    {
        if (canRight)
        {
            StartCoroutine(RightCooldown());
            rightAnim.SetBool("Punching", true);
            if (punchRoutine != null)
            {
                StopCoroutine(punchRoutine);
                punchRoutine = StartCoroutine(PunchCoroutine());
            }
            else
            {
                punchRoutine = StartCoroutine(PunchCoroutine());
            }
        }
        else
        {

        }
        
    }

    IEnumerator LeftCooldown()
    {
        canLeft = false;
        yield return new WaitForSeconds(5);
        canLeft = true;
    }

    IEnumerator RightCooldown()
    {
        canRight = false;
        yield return new WaitForSeconds(5);
        canRight = true;
    }

    IEnumerator PunchCoroutine()
    {
        trigger.SetActive(false);
        yield return new WaitForFixedUpdate();
        leftAnim.SetBool("Punching", false);
        rightAnim.SetBool("Punching", false);
        trigger.SetActive(true);
        yield return new WaitForSeconds(.5f);
        trigger.SetActive(false);
        punchRoutine = null;
    }
}
