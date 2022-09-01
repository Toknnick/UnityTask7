using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class HeroLogic : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _countOfHealth;

    private Vector2 _teleportPosition;
    private Rigidbody2D _rigidbody2D;
    private GroundSensor _groundSensor;
    private Animator _animator;
    private float _moveHorizontal;
    private float _timeSinceAttack;
    private int _currentAttack;
    private int _countOfCoins;
    private bool _isGrounded;
    private bool _isDefence;

    public void TeleportToSpawn()
    {
        transform.position = _teleportPosition;

        if (_countOfHealth == 0)
        {
            _animator.SetTrigger("Death");
            HeroLogic heroLogic = GetComponent<HeroLogic>();
            heroLogic.enabled = false;
            Debug.Log("Игра окончена!");
        }
        else
        {
            _countOfHealth--;
            Debug.Log("У вас осталось " + _countOfHealth + "хп");
            _animator.SetTrigger("Hurt");
        }
    }

    public void AddCoinsInBag()
    {
        _countOfCoins++;
        Debug.Log("Монет в сумке: " + _countOfCoins);
    }

    private void Start()
    {
        _timeSinceAttack = 0.0f;
        _currentAttack = 0;
        _groundSensor = transform.Find("Ground sensor").GetComponent<GroundSensor>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>(); 
        _teleportPosition = gameObject.transform.position;
        _isGrounded = false;
    }

    private void Update()
    {
        _timeSinceAttack += Time.deltaTime;

        if (_isGrounded == false && _groundSensor.IsGrounded())
        {
            _isGrounded = true;
            _animator.SetBool("Grounded", _isGrounded);
        }
        else if (_isGrounded == true && _groundSensor.IsGrounded() == false)
        {
            _isGrounded = false;
            _animator.SetBool("Grounded", _isGrounded);
        }

        if (_isDefence == false)
        {
            _moveHorizontal = Input.GetAxis("Horizontal");

            if (_moveHorizontal > 0)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (_moveHorizontal < 0)
                GetComponent<SpriteRenderer>().flipX = true;

            if (_isGrounded == true && Mathf.Abs(_moveHorizontal) > Mathf.Epsilon)
                _animator.SetInteger("AnimState", 1);
            else if (_isGrounded == true && Mathf.Abs(_moveHorizontal) < Mathf.Epsilon)
                _animator.SetInteger("AnimState", 0);

            _rigidbody2D.velocity = new Vector2(_moveHorizontal * _speed, _rigidbody2D.velocity.y);
            _animator.SetFloat("AirSpeedY", _rigidbody2D.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_isGrounded == true)
                {
                    _animator.SetTrigger("Jump");
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
                }
            }

            if (Input.GetMouseButtonDown(0) && _timeSinceAttack > 0.28f)
            {
                _currentAttack++;

                if (_currentAttack > 3)
                    _currentAttack = 1;

                _animator.SetTrigger("Attack" + _currentAttack);
                _timeSinceAttack = 0.0f;
            }
        }
        else
        {
            if (_isGrounded)
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _animator.SetTrigger("Block");
            _animator.SetBool("IdleBlock", true);
            _isDefence = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _animator.SetBool("IdleBlock", false);
            _isDefence = false;
        }
    }
}
