using System.Collections;
using UnityEngine;

public class KillingSensor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Input.GetMouseButton(0))
            StartCoroutine(Kill(other));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetMouseButton(0))
            StartCoroutine(Kill(other));
    }

    private IEnumerator Kill(Collider2D other)
    {
        if (other.gameObject.name.Contains("Enemy"))
            Destroy(other.gameObject, 0);

        Debug.Log("Враг убит");
        yield return null;
    }
}
