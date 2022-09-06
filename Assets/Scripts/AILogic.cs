using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AILogic : MonoBehaviour
{
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private int _speed;

    private int _numberOfMovePoint;
    private float _minDistanceToMovePoint;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _minDistanceToMovePoint = 0.2f;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _movePoints[_numberOfMovePoint].position) < _minDistanceToMovePoint)
        {
            _sprite.flipX = transform.position.x <= _movePoints[_numberOfMovePoint].position.x;

            _numberOfMovePoint++;
        }

        if (_numberOfMovePoint == _movePoints.Length)
            _numberOfMovePoint = 0;

        transform.position = Vector2.MoveTowards(transform.position, _movePoints[_numberOfMovePoint].position, _speed * Time.deltaTime);
    }
}
