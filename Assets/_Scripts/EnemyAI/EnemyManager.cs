using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private int minTimeBetweenSpawns, maxTimeBetweenSpawn, timeBetweenSpawns;
    [SerializeField] private int maxNumberOfEnemies;
    private int _currentNumberOfEnemies;

    [SerializeField] private Transform enemiesPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemies;

    private Coroutine _coroutine;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Obsolete]
    private void OnEnable()
    {
        _coroutine = StartCoroutine(SpawnEnemies());
    }

    [System.Obsolete]
    private void OnDisable()
    {
        StopCoroutine(SpawnEnemies());
    }

    [System.Obsolete]
    private IEnumerator SpawnEnemies()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            timeBetweenSpawns = Random.RandomRange(minTimeBetweenSpawns, maxTimeBetweenSpawn);
            SpawnEnemy();
        }
    }

    [System.Obsolete]
    private void SpawnEnemy()
    {
        if (_currentNumberOfEnemies >= maxNumberOfEnemies) return;

        GameObject auxGO;

        if(enemiesPool.childCount <= 0)
        {
            //Instanciar
            auxGO = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity, transform);
        }
        else
        {
            //Reciblar del pool
            //auxGO = enemiesPool.GetChild(0).gameObject;
            auxGO = enemiesPool.GetChild(Random.Range(0, enemiesPool.childCount)).gameObject;
            auxGO.transform.parent = transform;
            auxGO.transform.position = spawnPoints[Random.RandomRange(0, spawnPoints.Length)].position;
            auxGO.SetActive(true);
        }

        _currentNumberOfEnemies++;

    }
    public void DespawnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.parent = enemiesPool;
        _currentNumberOfEnemies--;
    }

}
