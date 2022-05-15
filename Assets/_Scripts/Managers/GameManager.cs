using UnityEngine;
using NoName.Utilities;
using System;

public class GameManager : Singleton<GameManager>
{
	public static event Action<GameState> OnBeforeStateChanged;
	public static event Action<GameState> OnAfterStateChanged;

	public GameState State { get; private set; }

	private void Start() => ChangeGameState(GameState.Starting);

    public void ChangeGameState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);
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
		OnAfterStateChanged?.Invoke(State);
    }
}
