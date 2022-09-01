using UnityEngine;
using UnityEngine.Events;

public class LineOfDeath : MonoBehaviour
{
    [SerializeField] private UnityEvent _hero;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "HeroKnight")
            _hero?.Invoke();
        else
            Destroy(collision.gameObject, 0);
    }
}
