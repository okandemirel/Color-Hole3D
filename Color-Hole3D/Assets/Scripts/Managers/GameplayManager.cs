using ScriptableScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameplayManager : MonoBehaviour
    {
        #region Singleton

        public static GameplayManager Instance;

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

        #region Public Variables

        public int LevelCollectableSize, AfterControlIncreasedCounterSize;

        #endregion


        #region Serialized Variables

        #endregion

        #region Private Variables

        #endregion

        private void Start()
        {
            LevelManager.Instance.LevelInstantiate += AssignLevelsRequiredCollectableSize;
        }

        public void IncreaseCounterAndControl()
        {
            AfterControlIncreasedCounterSize++;
            
            //UIManager.Instance.IncreaseUILevelBar.Invoke(AfterControlIncreasedCounterSize);
            
            if (AfterControlIncreasedCounterSize != LevelCollectableSize) return;
            LevelManager.Instance.LevelSuccess();
        }

        private void AssignLevelsRequiredCollectableSize(LevelScriptable newLevel)
        {
            LevelCollectableSize = newLevel.LevelCollectableCount;
        }
    }
}