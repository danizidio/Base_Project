using System;
using UnityEngine;
using UnityEngine.InputSystem;
using SaveLoadPlayerPrefs;

public class PlayerBehaviour : MonoBehaviour
{
    public event Action OnActing;
    public event Action OnPausing;

    Rigidbody2D rb;

    [SerializeField] int _maxLife;
    int _currentLife;

    [SerializeField] float _walkSpeed;
    float _runSpeed;
    float _move;
    float _moveX;
    float _moveY;

    bool _isRunning;

    [SerializeField] Transform _footDetector;

    [SerializeField] bool _canMove;
    public bool canMove { get { return _canMove; } }

    SaveLoad s;

    Animator _anim;

    private void Start()
    {
        _runSpeed = _walkSpeed * 2;

        rb = GetComponent<Rigidbody2D>();

        _canMove = true;

        _currentLife = _maxLife;

        _anim = GetComponentInChildren<Animator>();
    }

    void LateUpdate()
    {
        if (OnActing == null)
        {
            OnActing = Attacking;
        }

        if (!_isRunning)
        {
            _isRunning = false;
           _move = _walkSpeed;
        }

        if (_moveX == 0)
        {
           // _anim.SetBool("WALK", false);
        }
        else
        {
            //_anim.SetBool("WALK", true);
        }

        if (_canMove == false)
        {
            _moveX = 0;
            _moveY = 0;
        }

        rb.velocity = new Vector2(_moveX * _runSpeed, this.gameObject.transform.position.y);
    }

    #region - InputManager Buttons
    public void OnMove(InputAction.CallbackContext context)
    {
        if (_moveX == 1)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        if (_moveX == -1)
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }

        if (_canMove)
        {
            _moveX = context.ReadValue<Vector2>().x;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();

        if (_isRunning)
        {
            _move = _runSpeed;
        }
        else
        {
            _runSpeed = _walkSpeed;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(IsOnGround())
            {
                rb.AddForce(new Vector2(this.gameObject.transform.position.x, (2000 * 10) + Time.deltaTime), ForceMode2D.Force);
            }
        }
    }

    public void OnInteracting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnActing?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPausing?.Invoke();
        }
    }

    #endregion
    public void Attacking()
    {
        _anim.SetTrigger("ATTACK");
    }

    public bool CanMove(bool b)
    {
        return _canMove = b;
    }

    public void ReceiveDamage(int atk)
    {
        _currentLife -= atk;

        if (_currentLife <= 0)
        {
            GameBehaviour.OnNextGameState(GamePlayStates.GAMEOVER);
        }

        if (transform.localScale.x == 1)
        {
            rb.AddForce(new Vector2(-100, 0), ForceMode2D.Impulse);
        }

        if (transform.localScale.x == -1)
        {
            rb.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);
        }

        _anim.SetTrigger("DAMAGED");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //EnemyBehaviour e = collision.gameObject.GetComponent<EnemyBehaviour>();

        //if (e != null)
        //{
        //    e.ReceiveDamage(_atkModifier);
        //}
    }

    public bool IsOnGround()
    {
        return (Physics2D.Linecast(transform.position, _footDetector.position, 1 << LayerMask.NameToLayer("GROUND")) |
               (Physics2D.Linecast(transform.position, _footDetector.position, 1 << LayerMask.NameToLayer("FLOATINGPLATFORM")) |
               (Physics2D.Linecast(transform.position, _footDetector.position, 1 << LayerMask.NameToLayer("ACTORS")) |
               (Physics2D.Linecast(transform.position, _footDetector.position, 1 << LayerMask.NameToLayer("PUSHPULL"))))));
    }
}