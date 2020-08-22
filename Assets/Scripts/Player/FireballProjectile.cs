using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireballProjectile : MonoBehaviour
{
    private Rigidbody2D m_rb2D;
    private Vector2 movementDirection;
    private readonly float movementSpeed = 16.0f;
    private int fireballPower;

    public GameObject explosion;

    private void Start()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_rb2D.MovePosition(m_rb2D.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Treasure") && !collision.CompareTag("Dropoff"))
        {
            GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
            _explosion.GetComponent<ExplosionEffect>().PrimeExplosion(m_rb2D.position, fireballPower);
            _explosion.GetComponent<ExplosionEffect>().Explode();
            Destroy(gameObject);
        }
    }

    public void FireProjectile(Vector2 _moveDir, int _fireballPwr)
    {
        movementDirection = _moveDir;
        fireballPower = _fireballPwr;
    }
}
