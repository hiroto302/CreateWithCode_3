using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 複数のオブジェクト生成・応答 できるように実装する
// Obstacle の種類が増えてもうまく対応できる記述に改善していきたい
// _obstaclePool に全ての種類のObstacle を格納して、obstacle を要請された時に、そのList に格納されてるobjectをシャッフルして返すようにすればランダムに返すことができるかもしれない
public class SpawnManager : MonoSingletone<SpawnManager>
{
    // 生成する障害物
    [SerializeField]
    public GameObject[] obstaclePrefabs;
    // あらかじめ用意する障害物を管理するリスト
    [SerializeField]
    private List<GameObject> _obstaclePool;

    // 二種類目のobstacle を管理するリスト
    [SerializeField]
    private List<GameObject> _secondObstaclePool;

    // ContainerPrefab
    [SerializeField]
    private GameObject _obstacleContainerPrefab;
    // 生成するContainer の情報を取得するためにContainerPrefab 格納する List :
    // List は参照型なので ループ構文の時などで使用するとき挙動が変な時はこのことを思い出す [SerializeField]をつけると挙動が違う
    [SerializeField]
    private List<GameObject> _obstacleContainers;

    // 障害物がインスタンス化される時、親となるオブジェクト
    [SerializeField]
    private Transform _obstacleContainer;

    // 生成する位置
    private Vector3 _spawnPos = new Vector3(25, 0, 0);

    void Start()
    {
        // コンテイナーを作成
        GenerateObstacleContainers();
        // 各コンテナにObstacleObjectを格納
        GenerateObstacles(3, 0);
        GenerateObstacles(3, 1);

        // GameManager の状態が変わった時に実行するメソッド
        GameManager.Instance.OnGameStateChange += HandleGameStateChange;
    }

    void HandleGameStateChange(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if(currentState == GameManager.GameState.RUNNING && previousState == GameManager.GameState.PREGAME)
        {
            // 格納したobstacle をランダムに生成(種類に応じて)
            InvokeRepeating("SpawnRandomlyObstacle", 1.0f, 2.0f);
        }
    }

    // 格納されてるObstacle の種類の数だけ Containe を生成する
    void GenerateObstacleContainers()
    {
        for(int i = 0; i < obstaclePrefabs.Length; i++)
        {
            GameObject obstacleContainer = Instantiate(_obstacleContainerPrefab, transform.position, Quaternion.identity, transform);
            _obstacleContainers.Add(obstacleContainer);
        }
    }


    // Pool で管理する, 各obstaclePrefab(type) を 生成し, 各Container に格納するメソッド
    void GenerateObstacles(int amountOfObstacle, int type)
    {

        for(int i = 0; i < amountOfObstacle; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefabs[type]);
            obstacle.transform.parent = _obstacleContainers[type].transform;
            obstacle.SetActive(false);
            // オブジェクトのタイプによって格納するList を分ける
            if(type == 0)
            {
                _obstaclePool.Add(obstacle);
            }
            else if(type == 1)
            {
                _secondObstaclePool.Add(obstacle);
            }
        }
    }
    // 1つ目のObstacleを返す
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
        // newObstacle.transform.parent = _obstacleContainer;
        newObstacle.transform.parent = _obstacleContainers[0].transform;
        _obstaclePool.Add(newObstacle);
        return newObstacle;
    }
    // 2つ目のObstacleを返す
    public GameObject RequestSecondObstacle()
    {
        foreach(var obstacle in _secondObstaclePool)
        {
            if(obstacle.activeInHierarchy == false)
            {
                obstacle.SetActive(true);
                return obstacle;
            }
            // return obstacle; ここだと true でも呼び出してしまうから要注意
        }

        GameObject newObstacle = Instantiate(obstaclePrefabs[1]);
        newObstacle.transform.parent = _obstacleContainers[1].transform;
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

    public void SpawnRandomlyObstacle()
    {
        if(!PlayerController.gameOver)
        {
            int rnd = Random.Range(0, 2);
            if(rnd == 0)
            {
                GameObject obstacle = RequestObstacle();
                obstacle.transform.position = _spawnPos;
            }
            else
            {
                GameObject obstacle = RequestSecondObstacle();
                obstacle.transform.position = _spawnPos;
            }
        }
    }
}
