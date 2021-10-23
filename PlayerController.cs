using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModel;
    [SerializeField] private GameObject completeCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject gameOverCanvas;

    private Rigidbody2D _rb;
    private GameObject _finish;

    const float _speedXMultiplayer = 50f;
    private float _horizontalControl = 0f;
    private bool _isJump = false;
    private bool _isOnGround  = false;
    private bool _isTurnedRight = true;
    private bool _isFinish = false;
    private AudioSource _jumpSound;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish");
        _jumpSound = GetComponent<AudioSource>();
    }

    //Вызывется на каждом frame(кадре) около 50 раз в секунду
    void Update()
    {
        _horizontalControl = Input.GetAxis("Horizontal");
        animator.SetFloat("MoveSpeed", Mathf.Abs(_horizontalControl));
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _isJump = true;
            _jumpSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.F) && _isFinish)
        {
            _finish.SetActive(false);
            completeCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    //FixedUpdate вызывается перед или после вызова update, т.е. меняет физику нашего объекта
    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalControl * speedX * Time.fixedDeltaTime * _speedXMultiplayer, _rb.velocity.y);
        if(_isJump == true) 
        { 
            _rb.AddForce(new Vector2(0f, jumpForce));
            _isOnGround  = false;
            _isJump = false;
        }
        if(_horizontalControl > 0f && !_isTurnedRight)
            Flip();
        else if(_horizontalControl < 0f && _isTurnedRight)
            Flip();
    }

    void Flip()
    {
        _isTurnedRight = !_isTurnedRight;
        Vector3 playerScale = playerModel.localScale;
        playerScale.x *= -1;
        playerModel.localScale = playerScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LoosePoint"))
        {
            gameObject.SetActive(false);
            gameOverCanvas.SetActive(true);
        }
        if (other.gameObject.CompareTag("Ground"))
            _isOnGround  = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
            _isFinish = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
            _isFinish = false;
    }
}
