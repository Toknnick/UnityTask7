using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    private bool _isGrounded = false;

    public bool IsGrounded()
    {
        return _isGrounded;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _isGrounded = false;
    }
}
