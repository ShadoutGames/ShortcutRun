using UnityEngine;
using NaughtyAttributes;
using System;
using NoName.Utilities;

public class LevelManager : Singleton<LevelManager>
{
    #region SerializedFields

    [SerializeField, ReorderableList]
    private GameObject[] levels;

    #endregion

    #region Variables

    private GameObject currentLevel;

    #endregion

    #region Props

    #endregion

    #region Unity Methods

    private void Start()
    {
        currentLevel = Instantiate(levels[SaveManager.Instance.CurrentLevel], Vector3.zero, Quaternion.identity, transform);
        currentLevel.SetActive(true);
    }

    #endregion

    #region Methods

    public void InitNextLevel()
    {
        Destroy(currentLevel);
        currentLevel = Instantiate(levels[SaveManager.Instance.CurrentLevel], Vector3.zero, Quaternion.identity, transform);
        currentLevel.SetActive(true);

        LeanTween.cancelAll();

        GameManager.Instance.ChangeGameState(GameState.Starting);
    }

    public void InitCurrentLevel()
    {
        LeanTween.cancelAll();
        Destroy(currentLevel);
        currentLevel = Instantiate(levels[SaveManager.Instance.CurrentLevel], Vector3.zero, Quaternion.identity, transform);
        GameManager.Instance.ChangeGameState(GameState.Starting);
    }

    #endregion

    #region Callbacks

    #endregion
}