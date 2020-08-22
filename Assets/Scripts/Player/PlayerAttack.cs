using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform playerReticle;
    public SpriteRenderer reticleSprite;
    public Vector2 aimDirection;
    public GameObject fireballPrefab;
    public float fireRate = 2.0f;

    private readonly int fireballPower = 4;
    private readonly float m_AimReticleMaxDistance = 3.0f;
    private float nextFireballTime = 0.0f;

    public bool stunned = true;

    private void Update()
    {
        stunned = GetComponent<PlayerMovement>().stunned;
        playerReticle.position = GetComponent<Rigidbody2D>().position + aimDirection * m_AimReticleMaxDistance;
        reticleSprite.color = new Color(
            reticleSprite.color.r, 
            reticleSprite.color.g, 
            reticleSprite.color.b, 
            Mathf.Clamp(1.0f * Vector2.Distance(playerReticle.position, GetComponent<Rigidbody2D>().position) * m_AimReticleMaxDistance, 0.0f, 1.0f));
    }

    public void Fireball()
    {
        if (Time.time >= nextFireballTime && aimDirection != Vector2.zero && !stunned)
        {
            animator.SetTrigger("fire");
            AudioManager.instance.Play("Fireball");
            GameObject _fireball = Instantiate(fireballPrefab, transform.position, transform.rotation);
            _fireball.GetComponent<FireballProjectile>().FireProjectile(aimDirection.normalized, fireballPower);
            nextFireballTime = Time.time + 1.0f / fireRate;
        }

    }
}
