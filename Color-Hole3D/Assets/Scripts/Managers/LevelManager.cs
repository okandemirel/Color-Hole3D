using System.Collections;
using System.Collections.Generic;
using Data;
using ScriptableScripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Singleton

        public static LevelManager Instance;

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

        #region Unity Actions

        public UnityAction<LevelScriptable> LevelInstantiate = delegate { };
        public UnityAction StartNewLevel = delegate { };

        #endregion

        #region Static Variables

        private static int PreviousLevel;
        private static bool LevelRestarted;

        #endregion

        #region Public Variables

        [Header("Levels")] public List<LevelScriptable> Levels;

        [Header("Level Variables")] public int CurrentLevel;

        #endregion

        private IEnumerator Start()
        {
            GameData.OnLevelUpdate += ReloadScene;

            #region Get Level Data

            if (!LevelRestarted)
            {
                //currentLevel = GameData.CurrentLevel % levels.Count;
                CurrentLevel = GameData.CurrentLevel;


                if (CurrentLevel >= Levels.Count)
                {
                    CurrentLevel = Random.Range(0, Levels.Count);
                    if (CurrentLevel == PreviousLevel) CurrentLevel = Random.Range(0, Levels.Count);
                }
            }

            else CurrentLevel = PreviousLevel;

            PreviousLevel = CurrentLevel;

            #endregion

            #region Initialize Level

            LevelInstantiate.Invoke(Levels[CurrentLevel]);

            yield return new WaitForSeconds(.001f);
            UIManager.Instance.ActivateGameStartCanvas.Invoke();
            UIManager.Instance.NewLevelUITextAssign.Invoke(PlayerPrefs.GetInt("level") + 1);
            yield return new WaitForSeconds(.6f);
            EventManager.Instance.LevelTouchConditions.Invoke(true);

            #endregion
        }

        private void OnDisable()
        {
            GameData.OnLevelUpdate -= ReloadScene;
        }

        public void StartLevel()
        {
            StartNewLevel.Invoke();
        }

        public void LevelSuccess()
        {
            LevelRestarted = false;

            #region If Chest Bonus Added

            //if (GameData.Key != 2) GameData.CurrentLevel++;

            //else StartBonusChestCondition.Invoke();

            #endregion

            #region Normal

            GameData.CurrentLevel++;

            #endregion
        }

        public void LevelFailed()
        {
            LevelRestarted = true;
            GameData.OnLevelUpdate.Invoke();
        }

        private static void ReloadScene()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}