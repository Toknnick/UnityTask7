using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool IsGrounded {get; private set;}

    void OnTriggerEnter2D(Collider2D other)
    {
        IsGrounded = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IsGrounded = false;
    }
}
