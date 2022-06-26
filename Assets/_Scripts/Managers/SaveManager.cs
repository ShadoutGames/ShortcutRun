using System;
using NoName.Utilities;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    #region SerializedFields

    #endregion

    #region Variables

    private int currentLevel;

    #endregion

    #region Props

    public int CurrentLevel => currentLevel;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("Level");
        Debug.Log(PlayerPrefs.GetInt("Level"));
    }

    private void Start()
    {
        GameManager.AfterStateChanged += OnAfterStateChanged;
    }

    #endregion

    #region Methods

    public void SaveScore(int NewScore)
    {
        PlayerPrefs.SetInt("Score", NewScore);
    }

    #endregion

    #region Callbacks

    private void OnAfterStateChanged(GameState newState)
    {
        if (newState == GameState.Win)
        {
            currentLevel++;
            PlayerPrefs.SetInt("Level", currentLevel);
            Debug.Log(currentLevel);
        }
    }

    #endregion
}