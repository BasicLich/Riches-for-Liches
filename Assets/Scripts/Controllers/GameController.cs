using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{ 
    [Header("UI Management")]
    public TMP_Text remainingTreasure;

    [Header("Treasure Management")]
    public GameObject treasurePrefab;
    public List<Transform> treasureBaseSpawnpoints;
    public float maxSpawnOffset = 10.0f;
    public int treasurePilesToSpawn;
    public int coinPilesToSpawn;
    public static int treasureTotal;

    private void Start()
    {
        CreateHoard(treasurePilesToSpawn, coinPilesToSpawn);
    }

    private void Update()
    {
        treasureTotal = 0;
        foreach (TreasureContainer _t in TreasureContainer.allTreasureContainers)
        {
            treasureTotal += _t.treasureAmount;
        }
        if (treasureTotal <= 0 && GetComponent<EnemyWaveSpawner>().spawnState != EnemyWaveSpawner.SpawnState.Counting)
        {
            MySceneManager.instance.ChangeScene(MySceneManager.SceneState.LOSS);
        }
        remainingTreasure.text = treasureTotal.ToString();
    }

    void CreateHoard(int _pileCount, int _coinCount)
    {
        for (int _i = 0; _i < _pileCount; _i++)
        {
            int _sp = Random.Range(0, treasureBaseSpawnpoints.Count);
            float _spVarX = Random.Range(-1.0f, 1.0f);
            float _spVarY = Random.Range(-1.0f, 1.0f);
            float _spDist = Random.Range(0.0f, maxSpawnOffset);
            GameObject _t = Instantiate(treasurePrefab, treasureBaseSpawnpoints[_sp].position + new Vector3(_spVarX, _spVarY).normalized * _spDist, Quaternion.identity);
            _t.GetComponent<Treasure>().CreateTreasure(TreasureType.PileOfCoins);
        }

        for (int _i = 0; _i < _coinCount; _i++)
        {
            int _sp = Random.Range(0, treasureBaseSpawnpoints.Count);
            float _spVarX = Random.Range(-1.0f, 1.0f);
            float _spVarY = Random.Range(-1.0f, 1.0f);
            float _spDist = Random.Range(0.0f, maxSpawnOffset);
            GameObject _t = Instantiate(treasurePrefab, treasureBaseSpawnpoints[_sp].position + new Vector3(_spVarX, _spVarY).normalized * _spDist, Quaternion.identity);
            _t.GetComponent<Treasure>().CreateTreasure(TreasureType.Coins);
        }
    }

    private void OnDestroy()
    {
        Treasure.treasureList = new List<Treasure>();
        EnemyWaveSpawner.enemyBaseSpawnpoints = new List<Transform>();
    }

    private void OnDrawGizmos()
    {
        foreach (Transform _t in treasureBaseSpawnpoints)
        {
            Gizmos.DrawWireSphere(_t.position, maxSpawnOffset);
        }
    }
}
