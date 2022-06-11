using System;
using NoName.Utilities;
using UnityEngine;

namespace NoName
{
    public class UIManager : Singleton<UIManager>
    {
        #region SerializedFields

        [SerializeField]
        private GameObject startPanel;

        [SerializeField]
        private GameObject failPanel;

        [SerializeField]
        private GameObject waitPanel;

        [SerializeField]
        private GameObject finalPanel;

        #endregion

        #region Variables

        #endregion

        #region Props

        #endregion

        #region Unity Methods
        private void Start()
        {
            GameManager.AfterStateChanged += OnAfterStateChanged;

        }


        #endregion

        #region Methods

        private void CloseAllPanel()
        {
            startPanel.SetActive(false);
            waitPanel.SetActive(false);
            failPanel.SetActive(false);
            finalPanel.SetActive(false);
        }

        #endregion

        #region Callbacks

        private void OnAfterStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Starting:
                    CloseAllPanel();
                    startPanel.SetActive(true);
                    break;
                case GameState.Wait:
                    startPanel.SetActive(false);
                    waitPanel.SetActive(true);
                    break;
                case GameState.InGame:
                    waitPanel.SetActive(false);
                    break;
                case GameState.Win:
                    finalPanel.SetActive(true);
                    break;
                case GameState.Lose:
                    failPanel.SetActive(true);
                    break;
            }
        }

        #endregion
    }
}