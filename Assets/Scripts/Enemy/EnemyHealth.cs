using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemyDeath;

    public int maxHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<EnemyController>().DropTreasure();
        Instantiate(enemyDeath, transform.position, Quaternion.identity);
        if (Random.Range(0.0f, 1.0f) < 0.1f)
        {
            AudioManager.instance.Play("Soldier Death");
        }
        Destroy(gameObject);
    }
}
