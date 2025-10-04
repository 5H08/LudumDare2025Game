using UnityEngine;

public class MainManager : MonoBehaviour
{
    public float runLength = 300f;
    public float timeRemaining;

    public void StartRun()
    {
        timeRemaining = runLength;
    }

    public void EndRun()
    {

    }
}
