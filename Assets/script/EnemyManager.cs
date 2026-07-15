// 【重点】所有using必须放在脚本第一行，类定义的上方
using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [Header("生成配置")]
    // 三个怪物预制体，拖入Hierarchy里的Bat、Bird、Ghost
    public GameObject batPrefab;
    public GameObject birdPrefab;
    public GameObject ghostPrefab;
    // 生成点位Cube1
    public Transform spawnPoint;

    [Header("刷新时间参数")]
    // 初始生成间隔（秒）
    public float baseSpawnInterval = 5f;
    // 每生成一只怪物，间隔减少多少
    public float intervalReduceStep = 0.15f;
    // 最小刷新间隔，防止无限变快
    public float minSpawnInterval = 3f;

    // 当前实际刷新间隔
    private float currentSpawnInterval;
    // 计时器
    private float spawnTimer;
    // 存储场上所有怪物
    public List<GameObject> allEnemies = new List<GameObject>();

    void Start()
    {
        // 初始化刷新间隔
        currentSpawnInterval = baseSpawnInterval;
        spawnTimer = 0;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        // 计时器达到当前间隔，生成怪物
        if (spawnTimer >= currentSpawnInterval)
        {
            SpawnRandomEnemy();
            // 重置计时器
            spawnTimer = 0;
            // 缩短刷新间隔，不低于最小值
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - intervalReduceStep);
        }
    }

    // 随机生成一种怪物
    void SpawnRandomEnemy()
    {
        GameObject enemyPrefab = GetRandomEnemyPrefab();
        // 在Cube1位置生成怪物
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        // 加入怪物列表管理
        allEnemies.Add(newEnemy);
    }

    // 随机选取一个怪物预制体
    GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0: return batPrefab;
            case 1: return birdPrefab;
            case 2: return ghostPrefab;
            default: return batPrefab;
        }
    }

    // 怪物死亡时调用，从列表移除（给怪物移动脚本调用）
    public void RemoveEnemy(GameObject enemy)
    {
        if (allEnemies.Contains(enemy))
        {
            allEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }
}
