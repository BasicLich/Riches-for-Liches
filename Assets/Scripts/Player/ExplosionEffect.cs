using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private Vector2 explosionPosition;
    private float fireballRadius;
    private int fireballPower;

    public LayerMask damagingLayers;
    public float lifetime;
    public float elapsedTime;

    private void Start()
    {
        AudioManager.instance.Play("Explosion");
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
    public void PrimeExplosion(Vector2 _explosionPosition, int _fireballPower)
    {
        explosionPosition = _explosionPosition;
        fireballPower = _fireballPower;
        fireballRadius = _fireballPower / 2.0f;
    }

    public void Explode()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(explosionPosition, fireballRadius, damagingLayers);
        foreach (Collider2D _enemy in hitEnemies)
        {
            _enemy.GetComponent<EnemyHealth>().TakeDamage(fireballPower);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, fireballRadius);
    }
}
