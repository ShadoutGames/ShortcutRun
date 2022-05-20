using UnityEngine;
using NoName.Utilities;
using System;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> BeforeStateChanged;
    public static event Action<GameState> AfterStateChanged;

    public GameState State { get; private set; }

    private void Start() => ChangeGameState(GameState.Starting);

    public void ChangeGameState(GameState newState)
    {
        BeforeStateChanged?.Invoke(newState);
        State = newState;
        switch (State)
        {
            case GameState.Starting:
                break;
            case GameState.InGame:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }
        AfterStateChanged?.Invoke(State);
    }
}
