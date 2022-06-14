using System;
using NoName.Utilities;
using UnityEngine;
using TMPro;

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
        private GameObject ınGamePanel;

        [SerializeField]
        private GameObject succesPanel;

        [SerializeField]
        private TextMeshProUGUI countDown;

        [SerializeField]
        private TextMeshProUGUI currentCoin;

        #endregion

        #region Variables

        private float timer = 0;

        #endregion

        #region Props

        #endregion

        #region Unity Methods
        private void Start()
        {
            GameManager.AfterStateChanged += OnAfterStateChanged;
            ScoreManager.Instance.ScoreChanged += OnScoreChanged;
            CloseAllPanel();
        }


        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 3)
            {
                countDown.text = ((int)timer).ToString();
            }
        }


        #endregion

        #region Methods

        public void GetOnHold()
        {
            GameManager.Instance.ChangeGameState(GameState.Wait);
            Debug.Log(GameManager.Instance.State);
        }

        public void GetCurrenLevel()
        {
            //levelmanager yazılacak.
        }

        public void IncreaseWood()
        {
            CollectManager.Instance.Add();
        }
        // public void IncreaseTime()
        // {
        //     CollectManager.Instance.Add();
        // }

        private void CloseAllPanel()
        {
            startPanel.SetActive(false);
            waitPanel.SetActive(false);
            failPanel.SetActive(false);
            succesPanel.SetActive(false);
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
                    ınGamePanel.SetActive(true);
                    break;
                case GameState.Wait:
                    startPanel.SetActive(false);
                    waitPanel.SetActive(true);
                    timer = 3;
                    break;
                case GameState.InGame:
                    waitPanel.SetActive(false);
                    break;
                case GameState.Win:
                    succesPanel.SetActive(true);
                    break;
                case GameState.Lose:
                    failPanel.SetActive(true);
                    break;
            }
        }
        private void OnScoreChanged()
        {
            currentCoin.text = ScoreManager.Instance.CurrentScore.ToString();
        }

        #endregion
    }
}