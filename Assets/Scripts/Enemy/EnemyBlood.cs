using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlood : MonoBehaviour
{
    public float duration = 0.5f;
    private float timeElapsed = 0.0f;

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= duration)
        {
            Destroy(gameObject);
            return;
        }
    }
}
