using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCntroller : MonoBehaviour
{
    [SerializeField] PlayerController _player;

    void Start()
    {
        if(GameManager.Instance.CurrentState == GameManager.GameState.PREGAME)
        {
            Intro();
        }
    }

    // シーンの冒頭の処理
    void Intro()
    {
        StartCoroutine(IntroRoutine());
    }
    IEnumerator IntroRoutine()
    {
        // 初期位置
        _player.transform.position = new Vector3(-6.0f, 0, 0);
        // 移動先
        Vector3 gameStartPosition = Vector3.zero;
        // アニメーションスピード制御
        _player.gameObject.GetComponent<Animator>().speed = 0.2f;
        // 移動開始
        while(Vector3.Distance(_player.transform.position, gameStartPosition) > 0.2f)
        {
            _player.transform.position = Vector3.Lerp(_player.transform.position, gameStartPosition, 1.5f * Time.deltaTime);
            yield return null;
        }
        _player.transform.position = gameStartPosition;
        _player.gameObject.GetComponent<Animator>().speed = 1.0f;
        // ゲーム開始
        GameManager.Instance.StartGame();
    }
}
