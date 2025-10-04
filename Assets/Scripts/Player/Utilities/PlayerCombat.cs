using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Coroutine punchRoutine = null;
    
    public void LeftPunch()
    {
        print("Left Punch");

    }

    public void RightPunch()
    {
        print("Right Punch");
    }
}
