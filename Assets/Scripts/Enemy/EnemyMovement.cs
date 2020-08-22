using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public Animator animator;
    public Transform movementTarget;
    public Vector2 movementDirection;
    public float currentMovementSpeed;
    public float unencumberedMovementSpeed;
    public float encumberedMovementSpeed;
    public float nextWaypointDistance;
    public float seekerCheckrate = 1.0f;

    private Path m_Path;
    private int m_CurrentWaypoint;

    private Seeker m_Seeker;
    private Rigidbody2D m_rb2D;
    private EnemyController m_EnemyCont;

    // Start is called before the first frame update
    private void Start()
    {
        m_Seeker = GetComponent<Seeker>();
        m_rb2D = GetComponent<Rigidbody2D>();
        m_EnemyCont = GetComponent<EnemyController>();

        currentMovementSpeed = unencumberedMovementSpeed;

        InvokeRepeating("UpdatePath", 0.0f, seekerCheckrate);
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_Path == null)
            return;
        if (m_CurrentWaypoint >= m_Path.vectorPath.Count)
        {
            movementDirection = Vector2.zero;
            animator.SetFloat("enemySpeed", 0.0f);
            return;
        }

        movementDirection = ((Vector2)m_Path.vectorPath[m_CurrentWaypoint] - m_rb2D.position).normalized;
        animator.SetFloat("enemySpeed", movementDirection.magnitude);
        float _distance = Vector2.Distance(m_rb2D.position, m_Path.vectorPath[m_CurrentWaypoint]);

        if (_distance < nextWaypointDistance)
        {
            m_CurrentWaypoint++;
        }
    }

    private void FixedUpdate()
    {
        m_rb2D.MovePosition(m_rb2D.position + movementDirection * currentMovementSpeed * Time.fixedDeltaTime); 
    }   

    private void OnPathComplete(Path _p)
    {
        if (!_p.error)
        {
            m_Path = _p;
            m_CurrentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        if (m_Seeker.IsDone())
            if (movementTarget != null)
                m_Seeker.StartPath(m_rb2D.position, movementTarget.position, OnPathComplete);
            else
                FindMovementTarget();
    }

    public void FindMovementTarget()
    {
        if (m_EnemyCont.treasureContainer.treasureType == TreasureType.None)
        {
            //Find Closest Treasure
            Transform _bestTarget = null;
            float _closestDistance = Mathf.Infinity;
            Vector2 _currentPos = m_rb2D.position;
            foreach (Treasure _potentialTarget in Treasure.treasureList)
            {
                Vector2 _targetDir = (Vector2)_potentialTarget.gameObject.transform.position - _currentPos;
                float _dSqrToTarget = _targetDir.sqrMagnitude;
                if (_dSqrToTarget < _closestDistance)
                {
                    _closestDistance = _dSqrToTarget;
                    _bestTarget = _potentialTarget.gameObject.transform;
                }
            }
            movementTarget = _bestTarget;
        }
        else
        {
            //Find Closest Exit
            Transform _bestTarget = null;
            float _closestDistance = Mathf.Infinity;
            Vector2 _currentPos = m_rb2D.position;
            foreach (Transform _potentialTarget in EnemyWaveSpawner.enemyBaseSpawnpoints)
            {
                Vector2 _targetDir = (Vector2)_potentialTarget.position - _currentPos;
                float _dSqrToTarget = _targetDir.sqrMagnitude;
                if (_dSqrToTarget < _closestDistance)
                {
                    _closestDistance = _dSqrToTarget;
                    _bestTarget = _potentialTarget;
                }
            }
            movementTarget = _bestTarget;
        }
    }    
}
