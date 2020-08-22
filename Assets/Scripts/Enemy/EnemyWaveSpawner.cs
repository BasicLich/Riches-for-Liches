using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    }

    [System.Serializable]
    public struct Wave
    {
        public string waveName;
        public GameObject enemyPrefab;
        public int numberOfEnemies;
        public float spawnrate;
    }

    private int m_WaveIndex = 0;
    private float m_WaveCountdown = 0.0f;
    private float m_SearchCountdown = 1.0f;

    public SpawnState spawnState = SpawnState.Counting;
    public Wave[] spawnWaves;
    public float timeBetweenWaves = 5.0f;

    public TMP_Text popupMessage;
    public TMP_Text waveIndicator;

    [Header("Enemy Management")]
    public static List<Transform> enemyBaseSpawnpoints = new List<Transform>();

    private void Start()
    {
        m_WaveCountdown = timeBetweenWaves;
        Transform _childnodes = GetComponentInChildren<Transform>();
        foreach (Transform _childnode in _childnodes)
        {
            if (_childnode.name.Contains("[AIObjective]"))
            {
                enemyBaseSpawnpoints.Add(_childnode);
            }
        }
    }

    private void Update()
    {
        if (spawnState == SpawnState.Waiting)
        {
            if (!EnemyRemaining())
            {
                WaveComplete();
            }
            else
            {
                return;
            }    
        }

        if (m_WaveCountdown <= 0.0f)
        {
            if (spawnState != SpawnState.Spawning)
            {
                if (m_WaveIndex + 1 > spawnWaves.Length - 1)
                {
                    popupMessage.GetComponent<UIFade>().SetMessage("Final Wave Starting");
                    waveIndicator.text = "Final Wave";
                }
                else
                {
                    popupMessage.GetComponent<UIFade>().SetMessage("Wave " + (m_WaveIndex + 1) + " Starting");
                    waveIndicator.text = "Wave " + (m_WaveIndex + 1).ToString();
                }
                StartCoroutine(SpawnWave(spawnWaves[m_WaveIndex]));
            }
        }
        else
        {
            m_WaveCountdown -= Time.deltaTime;
        }
    }

    void WaveComplete()
    {
        popupMessage.GetComponent<UIFade>().SetMessage("Wave Complete");
        spawnState = SpawnState.Counting;
        m_WaveCountdown = timeBetweenWaves;

        if (m_WaveIndex + 1 > spawnWaves.Length - 1)
        {
            MySceneManager.instance.totalTreasureRemaining = GameController.treasureTotal;
            MySceneManager.instance.ChangeScene(MySceneManager.SceneState.WIN);
        }
        else
        {
            m_WaveIndex++;
        }
    }

    bool EnemyRemaining()
    {
        m_SearchCountdown -= Time.deltaTime;
        if (m_SearchCountdown <= 0.0f)
        {
            m_SearchCountdown = 1.0f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        spawnState = SpawnState.Spawning;
        for (int i = 0; i < _wave.numberOfEnemies; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1.0f / _wave.spawnrate);
        }
        spawnState = SpawnState.Waiting;
        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        int _sp = Random.Range(0, enemyBaseSpawnpoints.Count);
        Vector2 _vrdm = enemyBaseSpawnpoints[_sp].GetComponent<BoxCollider2D>().size;
        Vector2 _offset = Vector2.zero;
        float _offsetCal = Mathf.Max(_vrdm.x, _vrdm.y) / 4.0f;
        float _offsetVal = Random.Range(-_offsetCal, _offsetCal);
        if (_vrdm.x > _vrdm.y)
        {
            _offset = new Vector2(enemyBaseSpawnpoints[_sp].position.x + _offsetVal, enemyBaseSpawnpoints[_sp].position.y);
        }
        else
        {
            _offset = new Vector2(enemyBaseSpawnpoints[_sp].position.x, enemyBaseSpawnpoints[_sp].position.y + _offsetVal);
        }
        GameObject _t = Instantiate(_enemy, _offset, Quaternion.identity);
    }
}
