using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    [SerializeField]
    Transform player;
    public int maxEnemy;
    float spawnDelay;
    float timer;

    float maxX = 8;
    float maxZ = 6;

    public int enemyCount;
    public GameObject[] enemyPrefabs;

    List<GameObject>[] pools;
    [SerializeField]
    Transform poolParent;

    enum Direction
    {
        North,
        South,
        West,
        East
    }

    private void Awake()
    {
        instance = this;
        pools = new List<GameObject>[enemyPrefabs.Length];
        enemyCount = 0;
        Init();
    }
    
    private void Init()
    {
        spawnDelay = 0.5f;
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnDelay && enemyCount < maxEnemy)
        {
            timer = 0f;
            Spawn();
        }
    }

    public void Spawn()
    {
        GameObject newEnemy = Get(0);
        newEnemy.transform.position = RandomPosition();
        float health = 100f;
        float damage = 0f;
        float speed = 5f;
        newEnemy.GetComponent<Enemy>().Setup(health,damage,speed);
        enemyCount++;
    }

    private Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3();

        Direction direction = (Direction)Random.Range(0, 4);

        switch (direction)
        {
            case Direction.North:
                pos.x = Random.Range(player.transform.position.x - maxX, player.transform.position.x + maxX);
                pos.z = player.transform.position.z + 6f;
                break;
            case Direction.South:
                pos.x = Random.Range(player.transform.position.x - maxX, player.transform.position.x + maxX);
                pos.z = player.transform.position.z - 6f;
                break;
            case Direction.West:
                pos.x = player.transform.position.x - 8f;
                pos.z = Random.Range(player.transform.position.z - maxZ, player.transform.position.y + maxZ);
                break;
            case Direction.East:
                pos.x = player.transform.position.x + 8f;
                pos.z = Random.Range(player.transform.position.z - maxZ, player.transform.position.y + maxZ);
                break;
        }

        return pos;
    }

    private GameObject Get(int i)
    {
        GameObject select = null;

        foreach (GameObject enemy in pools[i])
        {
            if (!enemy.activeSelf)
            {
                select = enemy;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(enemyPrefabs[i], transform);
            pools[i].Add(select);
        }

        return select;
    }

}
