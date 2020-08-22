using System;
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
        public UnityAction CloseJoystickCanvasCauseLevelConditionHappened = delegate { };

        #endregion

        private void Start()
        {
            LevelManager.Instance.NewLevelUIAssign = SetNewLevelsText;
            LevelManager.Instance.LevelUIInitialize = ActivateGameCanvas;
        }

        public void StartLevel()
        {
            LevelManager.Instance.StartNewLevel.Invoke();
        }

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