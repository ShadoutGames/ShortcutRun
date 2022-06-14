using System;
using UnityEngine;

namespace NoName
{
    public class HandEntitiy : MonoBehaviour
    {
        #region SerializedFields

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
        private void MoveEntitiy()
        {
            transform.LeanMoveLocalX(-250f, 0.5f).setOnComplete(() => transform.LeanMoveLocalX(250f, 0.5f).setLoopPingPong());
        }

        #endregion

        #region Callbacks
        private void OnAfterStateChanged(GameState newState)
        {
            if (newState == GameState.Wait)
            {
                MoveEntitiy();
            }
        }

        #endregion
    }
}