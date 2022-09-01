using UnityEngine;

public class AILogic : MonoBehaviour
{
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private int _speed;

    private int _numberOfMovePoint;

    void Update()
    {
        if (Vector2.Distance(transform.position, _movePoints[_numberOfMovePoint].position) < 0.2f)
        {
            if (transform.position.x > _movePoints[_numberOfMovePoint].position.x)
                GetComponent<SpriteRenderer>().flipX = false;
            else
                GetComponent<SpriteRenderer>().flipX = true;

            _numberOfMovePoint++;
        }

        if (_numberOfMovePoint == _movePoints.Length)
            _numberOfMovePoint = 0;

        transform.position = Vector2.MoveTowards(transform.position, _movePoints[_numberOfMovePoint].position, _speed * Time.deltaTime);
    }
}
