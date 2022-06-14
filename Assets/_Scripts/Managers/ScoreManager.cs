using System;
using NoName.Utilities;
using UnityEngine;

namespace NoName
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        #region SerializedFields

        #endregion

        #region Variables

        private int currentScore;

        #endregion

        #region Props

        public int CurrentScore => currentScore;

        #endregion

        #region Actions

        public event Action ScoreChanged;

        #endregion

        #region Unity Methods

        public void IncreaseCurrentScore(int scoreValue)
        {
            ScoreChanged?.Invoke();
            currentScore += scoreValue;
        }
        #endregion

        #region Methods

        #endregion

        #region Callbacks

        #endregion
    }
}