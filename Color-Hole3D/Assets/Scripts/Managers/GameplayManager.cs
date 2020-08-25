using System.Collections;
using CollisionControl;
using DG.Tweening;
using Movement;
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

        public bool
            AdvertisementPlayed, //If there was an AdManager in game this will be added to there.
            ZoneChangeable,
            PassZones;

        #endregion

        #region Serialized Variables

        [SerializeField] private ParticleSystem levelCompleteConfetti;
        [SerializeField] private Collider underLevelCollider;
        [SerializeField] private MoveShaderedHole holeObject;

        #endregion

        #region Private Variables

        private Camera _mainCam;

        #endregion

        private void Start()
        {
            IsGameOver = false;
            LevelManager.Instance.SetLevelVariables += AssignLevelsRequiredCollectableSize;
            underLevelCollider = FindObjectOfType<BottomColliderControl>().GetComponent<Collider>();
            holeObject = FindObjectOfType<MoveShaderedHole>();
            _mainCam = Camera.main;
        }

        public void IncreaseCounterAndControl()
        {
            AfterControlIncreasedCounterSize++;

            UIManager.Instance.IncreaseUILevelBar.Invoke();

            if (AfterControlIncreasedCounterSize == ZoneCollectable && !PassZones)
            {
                if (ZoneChangeable)
                {
                    Debug.Log("Zone Conditions Starts");
                    //Move to the next point
                    var seq = DOTween.Sequence();
                    seq.Append(holeObject.transform.DOLocalMoveX(0, .4f))
                        .Append(holeObject.transform.DOLocalMoveZ(holeObject.transform.localPosition.z + 1f, .6f))
                        .Join(_mainCam.transform.DOLocalMoveZ(_mainCam.transform.localPosition.z + 1.6f, .6f))
                        .Join(levelCompleteConfetti.transform.DOLocalMoveZ(
                            levelCompleteConfetti.transform.localPosition.z + 1.6f, .6f))
                        .OnComplete(() => holeObject.AfterZoneChangeMovementLimits = true);
                    PassZones = true;
                    ZoneChangeable = false;
                }
            }

            if (AfterControlIncreasedCounterSize != LevelCollectableSize)
                return;

            levelCompleteConfetti.Play();

            underLevelCollider.enabled = false;

            StartCoroutine(GoToNextLevel());
        }

        private void AssignLevelsRequiredCollectableSize(LevelScriptable newLevel)
        {
            LevelCollectableSize = newLevel.LevelCollectablesCount;
            ZoneCollectable = newLevel.Zone1CollectableCount;
            ZoneChangeable = newLevel.ZoneChangeable;
        }

        private IEnumerator GoToNextLevel()
        {
            yield return new WaitForSeconds(2.2f);
            LevelManager.Instance.LevelSuccess();
        }
    }
}