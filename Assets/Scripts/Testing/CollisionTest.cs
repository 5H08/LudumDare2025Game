using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("Entered Trigger");
    }
}
