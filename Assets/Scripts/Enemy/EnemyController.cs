using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TreasureContainer))]
public class EnemyController : MonoBehaviour
{
    public GameObject treasurePrefab;
    public TreasureContainer treasureContainer;
    private EnemyMovement m_EnemyMov;
    public SpriteRenderer notificationSprite;

    private void Start()
    {
        m_EnemyMov = GetComponent<EnemyMovement>();
        treasureContainer.treasureType = TreasureType.None;
        notificationSprite.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Treasure") && treasureContainer.treasureType == TreasureType.None)
        {
            treasureContainer.treasureType = collision.GetComponent<TreasureContainer>().treasureType;
            treasureContainer.treasureAmount = collision.GetComponent<TreasureContainer>().treasureAmount;
            collision.GetComponent<Treasure>().LootTreasure();
            m_EnemyMov.currentMovementSpeed = m_EnemyMov.encumberedMovementSpeed;
            m_EnemyMov.FindMovementTarget();
            notificationSprite.enabled = true;
        }
        else if (collision.CompareTag("Dropoff") && treasureContainer.treasureType != TreasureType.None)
        {
            treasureContainer.treasureType = TreasureType.None;
            treasureContainer.treasureAmount = 0;
            m_EnemyMov.movementTarget = null;
            m_EnemyMov.currentMovementSpeed = m_EnemyMov.unencumberedMovementSpeed;
            m_EnemyMov.FindMovementTarget();
            notificationSprite.enabled = false;
        }
    }

    public void DropTreasure()
    {
        if(treasureContainer.treasureType != TreasureType.None)
        {
            GameObject _t = Instantiate(treasurePrefab, transform.position, transform.rotation);
            _t.GetComponent<Treasure>().CreateTreasure(treasureContainer.treasureType);
        }
    }
}
