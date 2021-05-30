using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState
    {
        PREGAME,    // 開始前
        RUNNING     // 開始後 Playerの操作が始まる
    }

    // 第一引数 現在の状態, 第二引数 以前の状態
    public event Action<GameState, GameState> OnGameStateChange;

    private GameState _currentState;   // 現在の状態

    public GameState CurrentState
    {
        get { return _currentState; }
        private set { _currentState = value; }
    }


    public void UpdateState(GameState state)
    {
        GameState previousGameState = _currentState;
        _currentState = state;

        OnGameStateChange(_currentState, previousGameState);
    }

    public void GameStart()
    {
        UpdateState(GameState.RUNNING);
    }
}
