using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerAttack m_playerAtt;
    private Rigidbody2D m_rb2D;
    private Vector2 m_PlayerVelocity;
    private bool facingRight = true;

    public Animator animator;
    public Vector2 movementDirection;
    public float movementSpeed;

    public bool stunned = false;
    public float stunTimer = 1.0f;
    public float stunTimeElapsed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
        m_playerAtt = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        float _movingMagnitude = movementDirection.x;
        float _aimingMagnitude = m_playerAtt.aimDirection.x;
        if (movementDirection != Vector2.zero && !stunned)
        {
            m_PlayerVelocity = movementDirection.normalized * movementSpeed;
            animator.SetFloat("playerSpeed", m_PlayerVelocity.magnitude);
            if (_movingMagnitude > 0.0f && !facingRight)
            {
                if (_aimingMagnitude > 0.0f || _aimingMagnitude == 0.0f)
                    FlipAnimations();
            }
            else if (_movingMagnitude > 0.0f && facingRight && _aimingMagnitude < 0.0f)
            {
                FlipAnimations();
            }
            else if (_movingMagnitude < 0.0f && facingRight)
            {
                if (_aimingMagnitude < 0.0f || _aimingMagnitude == 0.0f)
                    FlipAnimations();
            }
            else if (_movingMagnitude < 0.0f && !facingRight && _aimingMagnitude > 0.0f)
            {
                FlipAnimations();
            }
        }
        else
        {
            m_PlayerVelocity *= 0.975f;
            animator.SetFloat("playerSpeed", 0.0f);
            if (_aimingMagnitude > 0.0f && !facingRight)
            {
                FlipAnimations();
            }
            else if (_aimingMagnitude < 0.0f && facingRight)
            {
                FlipAnimations();
            }
        }

        if (stunned)
        {
            stunTimeElapsed += Time.deltaTime;
            if (stunTimeElapsed >= stunTimer)
            {
                stunTimeElapsed = 0.0f;
                stunned = false;
            }
        }
    }

    private void FixedUpdate()
    {
        m_rb2D.MovePosition(m_rb2D.position + m_PlayerVelocity * Time.fixedDeltaTime);
    }
    
    private void FlipAnimations()
    {
        facingRight = !facingRight;
        Vector2 _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject _colTarget = collision.collider.gameObject;
        if (_colTarget.CompareTag("Enemy"))
        {
            _colTarget.GetComponent<EnemyHealth>().TakeDamage(4);
            animator.SetTrigger("damage");
            stunned = true;
            AudioManager.instance.Play("Impact");
        }
    }
}
