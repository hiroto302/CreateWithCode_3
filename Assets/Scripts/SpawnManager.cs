using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingletone<SpawnManager>
{
    // 生成する障害物
    [SerializeField]
    public GameObject[] obstaclePrefabs;
    // あらかじめ用意する障害物を管理するリスト
    [SerializeField]
    private List<GameObject> _obstaclePool;
    // 障害物がインスタンス化される時、親となるオブジェクト
    [SerializeField]
    private Transform _obstacleContainer;

    // 生成する位置
    private Vector3 _spawnPos = new Vector3(25, 0, 0);

    void Start()
    {
        GenerateObstacles(5, 0);
        InvokeRepeating("SpawnObstacle", 1.0f, 2.0f);
    }
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     SpawnObstacle();
        // }
    }

    // Pool で管理する, 各obstaclePrefab(type) を 生成するメソッド
    void GenerateObstacles(int amountOfObstacle, int type)
    {
        for(int i = 0; i < amountOfObstacle; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefabs[type]);
            obstacle.transform.parent = _obstacleContainer;
            obstacle.SetActive(false);
            _obstaclePool.Add(obstacle);
        }
    }

    public GameObject RequestObstacle()
    {
        foreach(var obstacle in _obstaclePool)
        {
            if(obstacle.activeInHierarchy == false)
            {
                obstacle.SetActive(true);
                return obstacle;
            }
            // return obstacle; ここだと true でも呼び出してしまうから要注意
        }

        GameObject newObstacle = Instantiate(obstaclePrefabs[0]);
        newObstacle.transform.parent = _obstacleContainer;
        _obstaclePool.Add(newObstacle);
        return newObstacle;
    }

    public void SpawnObstacle()
    {
        if(!PlayerController.gameOver)
        {
            // Instantiate(RequestObstacle, _spawnPos, Quaternion.identity);
            GameObject obstacle = RequestObstacle();
            obstacle.transform.position = _spawnPos;
        }
    }


}
