using System.Collections;
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

        #region Static Variables

        public static bool IsGameOver;

        #endregion

        #region Public Variables

        public int LevelCollectableSize, AfterControlIncreasedCounterSize, ZoneCollectable;
        public bool AdvertisementPlayed; //If there was an AdManager in game this will be added to there.

        #endregion

        #region Serialized Variables

        [SerializeField] private ParticleSystem levelCompleteConfetti;

        #endregion

        #region Private Variables

        #endregion

        private void Start()
        {
            IsGameOver = false;
            LevelManager.Instance.SetLevelVariables += AssignLevelsRequiredCollectableSize;
        }

        public void IncreaseCounterAndControl()
        {
            AfterControlIncreasedCounterSize++;

            UIManager.Instance.IncreaseUILevelBar.Invoke();

            if (AfterControlIncreasedCounterSize != LevelCollectableSize) return;

            levelCompleteConfetti.Play();

            StartCoroutine(GoToNextLevel());
        }

        private void AssignLevelsRequiredCollectableSize(LevelScriptable newLevel)
        {
            LevelCollectableSize = newLevel.LevelCollectablesCount;
        }

        private IEnumerator GoToNextLevel()
        {
            yield return new WaitForSeconds(2.2f);
            LevelManager.Instance.LevelSuccess();
        }
    }
}