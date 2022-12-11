using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private BasePlayer _player;
    [SerializeField] private GameObject _enemyPoolsParant;
    [SerializeField] private float _spawnInterval, _spawnIntervalMax, _stepTime;

    [SerializeField] private GameObject[] _spawnZones;
    [SerializeField] private int _stepGold;

    private Enemy[] _enemyPrefabs;
    private ObjectPool<Enemy>[] _enemyPools;
    private Coroutine _spawningRandomEnemy;

    private List<int> _allChances;
    private int _chancesSum;

    private static EnemySpawner _instance;
    public static EnemySpawner GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        EventManager.OnEnemyReturnToPool.AddListener(ReturnEnemyToPool);
        EventManager.OnGoldChanged.AddListener(CalculateSpawnInterval);

    }

    private void Start()
    {
        StartSpawning();
    }

    private void CalculateSpawnInterval(int value)
    {
        _spawnInterval = _spawnIntervalMax - (UIManager.TotalEarned / _stepGold) * _stepTime;
        if (_spawnInterval <= 0.1f)
        {
            _spawnInterval = 0.2f;
        }
    }

    public void StopSpawning()
    {
        StopCoroutine(_spawningRandomEnemy);
    }

    public void StartSpawning()
    {
        PreparePrefabs();
        

        _spawningRandomEnemy = StartCoroutine(SpawnRandomEnemyFromPool());
    }

    private void PreparePrefabs()
    {
        _enemyPrefabs = Resources.LoadAll<Enemy>("Prefabs/EnemyPrefabs");

        CreatePools();
    }

    private void CreatePools()
    {
        _enemyPools = new ObjectPool<Enemy>[_enemyPrefabs.Length];

        _allChances = new List<int>();
        _chancesSum = 0;

        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            _enemyPools[(int)_enemyPrefabs[i].Type] = new ObjectPool<Enemy>(createFunc: () => new Enemy(), actionOnGet: (obj) => obj.gameObject.SetActive(true), actionOnRelease: (obj) => obj.gameObject.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), false, defaultCapacity: 50);

            AddChances(_enemyPrefabs[i].Weight);
            FillPool(_enemyPools[(int)_enemyPrefabs[i].Type], _enemyPrefabs[i], 50);
        }
    }

    private void AddChances(int weight)
    {
        _chancesSum += weight;
        _allChances.Add(weight);
    }

    private void FillPool(ObjectPool<Enemy> pool, Enemy prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy enemy = Instantiate(prefab);
            enemy.transform.SetParent(_enemyPoolsParant.transform);
            pool.Release(enemy);
        }
    }

    private IEnumerator SpawnRandomEnemyFromPool()
    {
        while (true)//game running
        {
            SpawnPrefabFromPool(GetWeightRandomPrefabFromPool(), GetRandomSpawnPositionInZones()); //GetRandomSpawnPosition(_arenaWidth, _arenaHeight));
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnPrefabFromPool(ObjectPool<Enemy> pool, Vector3 position)
    {
        Enemy enemy = pool.Get();
        //enemy.transform.position = position;
        enemy.SetPosition(position);
        enemy.SetTarget(_player);
        enemy.GetComponent<CharacterController>().enabled = true;
    }

    private ObjectPool<Enemy> GetWeightRandomPrefabFromPool()
    {
        int value = Random.Range(0, _chancesSum);
        int sum = 0;

        for (int i = 0; i < _allChances.Count; i++)
        {
            sum += _allChances[i];
            if (value < sum)
            {
                return _enemyPools[i];
            }
        }

        return _enemyPools[_enemyPools.Length - 1];
    }

    private Vector3 GetRandomSpawnPositionInZones()
    {
        Transform zoneTransform = _spawnZones[Random.Range(0, _spawnZones.Length)].transform;
        Vector3 pos = new Vector3();
        pos.x = Random.Range(0, zoneTransform.localScale.x) + zoneTransform.position.x - zoneTransform.localScale.x / 2;
        pos.z = Random.Range(0, zoneTransform.localScale.z) + zoneTransform.position.z - zoneTransform.localScale.z / 2;
        pos.y = 0;

        Collider[] colliders = Physics.OverlapSphere(pos, 0.5f);
  
        if (colliders.Length > 1)  
        {
            //Debug.Log("Что-то есть");
            return GetRandomSpawnPositionInZones();
        }
        else
        {
            //Debug.Log("Ничего нет");
            return pos;
        }  
    }

    private void ReturnEnemyToPool(Enemy enemy)
    {
        enemy.GetComponent<CharacterController>().enabled = false;
        _enemyPools[(int)enemy.Type].Release(enemy);
    }
}


