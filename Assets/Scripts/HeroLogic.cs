using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class HeroLogic : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _countOfHealth;
    [SerializeField] private GroundSensor _groundSensor;

    private AnimatorHeroKnight _animatorHeroKnight;
    private Vector2 _teleportPosition;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private float _moveHorizontal;
    private float _timeSinceAttack;
    private float _timeOfAttack;
    private int _currentAttack;
    private int _startingAttack;
    private int _maxCountOfAttack;
    private int _countOfCoins;
    private bool _isGrounded;
    private bool _isDefence;

    public void TeleportToSpawn()
    {
        transform.position = _teleportPosition;

        if (_countOfHealth == 0)
        {
            _animator.SetTrigger(_animatorHeroKnight.Death);
            HeroLogic heroLogic = GetComponent<HeroLogic>();
            heroLogic.enabled = false;
            Debug.Log("Игра окончена!");
        }
        else
        {
            _countOfHealth--;
            Debug.Log("У вас осталось " + _countOfHealth + "хп");
            _animator.SetTrigger(_animatorHeroKnight.Hurt);
        }
    }

    public void AddCoinsInBag()
    {
        _countOfCoins++;
        Debug.Log("Монет в сумке: " + _countOfCoins);
    }

    private void Start()
    {
        _currentAttack = 0;
        _maxCountOfAttack = 3;
        _startingAttack = 1;
        _timeOfAttack = 0.28f;
        _timeSinceAttack = 0;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _animatorHeroKnight = GetComponent<AnimatorHeroKnight>();
        _teleportPosition = gameObject.transform.position;
        _isGrounded = false;
        _isDefence = false;
    }

    private void Update()
    {
        _timeSinceAttack += Time.deltaTime;

        if (_isGrounded == false && _groundSensor.IsGrounded)
        {
            _isGrounded = true;
            _animator.SetBool(_animatorHeroKnight.Grounded, _isGrounded);
        }
        else if (_isGrounded == true && _groundSensor.IsGrounded == false)
        {
            _isGrounded = false;
            _animator.SetBool(_animatorHeroKnight.Grounded, _isGrounded);
        }

        if (_isDefence == false)
        {
            _moveHorizontal = Input.GetAxis("Horizontal");

            if (_moveHorizontal > 0)
                _sprite.flipX = false;
            else if (_moveHorizontal < 0)
                _sprite.flipX = true;

            if (_isGrounded == true && Mathf.Abs(_moveHorizontal) > Mathf.Epsilon)
                _animator.SetInteger(_animatorHeroKnight.AnimState, 1);
            else if (_isGrounded == true && Mathf.Abs(_moveHorizontal) < Mathf.Epsilon)
                _animator.SetInteger(_animatorHeroKnight.AnimState, 0);

            _rigidbody2D.velocity = new Vector2(_moveHorizontal * _speed, _rigidbody2D.velocity.y);
            _animator.SetFloat(_animatorHeroKnight.AirSpeedY, _rigidbody2D.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_isGrounded == true)
                {
                    _animator.SetTrigger(_animatorHeroKnight.Jump);
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
                }
            }

            if (Input.GetMouseButtonDown(0) && _timeSinceAttack > _timeOfAttack)
            {
                _currentAttack++;

                _animator.SetTrigger(_animatorHeroKnight.ChangeAttackMod(_currentAttack));
                _timeSinceAttack = 0;

                if (_currentAttack > _maxCountOfAttack)
                    _currentAttack = _startingAttack;
            }
        }
        else
        {
            if (_isGrounded)
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _animator.SetTrigger(_animatorHeroKnight.Block);
            _animator.SetBool(_animatorHeroKnight.IdleBlock, true);
            _isDefence = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _animator.SetBool(_animatorHeroKnight.IdleBlock, false);
            _isDefence = false;
        }
    }
}
