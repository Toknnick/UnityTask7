using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] private UnityEvent _hero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out HeroLogic heroLogic))
        {
            Destroy(gameObject, 0);
            _hero?.Invoke();
        }
    }
}
