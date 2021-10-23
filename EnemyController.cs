using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chasingSpeed = 2f;
    [SerializeField] private float timeToWait = 3f;
    [SerializeField] private float timeToChase = 3f;
    [SerializeField] private float minDistanceToPlayer = 1.5f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform enemyModel;

    private Rigidbody2D _rb;
    private Transform _playerTransorm;
    private Vector2 _leftPosition;
    private Vector2 _rightPosition;
    private Vector2 _nextPoint;

    private bool _isTurnedRight = true;
    private bool _isWait = false;
    private bool _isChasingPlayer = false;

    private float _waitTime;
    private float _chaseTime;
    private float _walkSpeed;
    private float _distance;

    public bool IsTurnedRight { get => _isTurnedRight; }
  
    public void StartChasingPlayer()
    {
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeed = chasingSpeed;
    }
    private void Start()
    {
        _playerTransorm = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _leftPosition = transform.position;
        _rightPosition = _leftPosition + Vector2.right * walkDistance;
        _waitTime = timeToWait;
        _chaseTime = timeToChase;
        _walkSpeed = patrolSpeed;
    }

    private void Update()
    {
        if(_isChasingPlayer)
            StartChasingTimer();
        
        if (_isWait && !_isChasingPlayer)
        {
            StartWaitTimer();
            animator.SetFloat("MushroomSpeed", 0f);
        }
        else
            animator.SetFloat("MushroomSpeed", Mathf.Abs(patrolSpeed));

        if (ShouldEnemyWait())
        {
            _isWait = true;
        }
    }
    private void StartWaitTimer()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _isWait = false;
            _waitTime = timeToWait;
            Flip();
        }
    }
    private bool ShouldEnemyWait()
    {
        bool isOutOfRightPoint = _isTurnedRight && transform.position.x >= _rightPosition.x;
        bool isOutOfLeftPoint = !_isTurnedRight && transform.position.x <= _leftPosition.x;

        return isOutOfLeftPoint || isOutOfRightPoint;
    }
    private void FixedUpdate()
    {
        _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;
        if (_isChasingPlayer && Mathf.Abs(DistanceToPlayer()) < minDistanceToPlayer)
            return;

        if(_isChasingPlayer)
        {
            ChasePlayer();
        }

        if (!_isWait && !_isChasingPlayer)
            Patrol();
    }
    private void Patrol()
    {
        if (!_isTurnedRight)
        {
            _nextPoint.x *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);

    }
    private void ChasePlayer()
    {
        _distance = DistanceToPlayer();
        if(_distance < 0)
        {
            _nextPoint *= -1;
        }
        if (_distance > 0.2f && !_isTurnedRight)
            Flip();
        else if (_distance < 0.2 && _isTurnedRight)
            Flip();
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }
    public float DistanceToPlayer()
    {
        return _playerTransorm.position.x - transform.position.x;
    }
    private void StartChasingTimer()
    {
        _chaseTime -= Time.deltaTime;
        if(_chaseTime < 0f)
        {
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
            _walkSpeed = patrolSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftPosition, _rightPosition);
    }
    public void Flip()
    {
        _isTurnedRight = !_isTurnedRight;
        Vector3 playerScale = enemyModel.localScale;
        playerScale.x *= -1;
        enemyModel.localScale = playerScale;
    }
}