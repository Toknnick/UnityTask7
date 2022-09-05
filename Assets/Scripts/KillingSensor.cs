using UnityEngine;

public class KillingSensor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Input.GetMouseButton(0))
            Kill(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetMouseButton(0))
            Kill(other);
    }

    private void Kill(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>())
            Destroy(other.gameObject, 0);
    }
}
