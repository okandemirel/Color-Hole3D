using System;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton

        public static UIManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion

        #region UnityActions

        public UnityAction<int> NewLevelUITextAssign = delegate { };
        public UnityAction ActivateGameStartCanvas = delegate { };
        public UnityAction IncreaseUILevelBar = delegate { };

        #endregion

        private void SetNewLevelsText(int newLevelValue)
        {
            NewLevelUITextAssign.Invoke(newLevelValue);
        }

        private void ActivateGameCanvas()
        {
            ActivateGameStartCanvas.Invoke();
        }
    }
}