using System;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIBehaviour
{
    public class UIInitialize : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private Canvas levelConditionsCanvas;
        [SerializeField] private Image progressBarImage;
        [SerializeField] private TextMeshProUGUI levelTextLeftSide, levelTextRightSide;

        #endregion

        #region Private Variables

        private Tween _progressBarTween;

        #endregion

        private void Start()
        {
            UIManager.Instance.NewLevelUITextAssign = AssignLevelText;
            UIManager.Instance.IncreaseUILevelBar = IncreaseFillAmount;
            EventManager.Instance.LevelCanvasConditionsForOpening = ActivateLevelConditionCanvas;
            EventManager.Instance.LevelCanvasConditionsForClosing = DeactivateLevelConditionCanvas;
        }
        
        private void ActivateLevelConditionCanvas(bool value)
        {
            levelConditionsCanvas.gameObject.SetActive(true);
            switch (value)
            {
                case true:
                    levelConditionsCanvas.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case false:
                    levelConditionsCanvas.transform.GetChild(1).gameObject.SetActive(true);
                    break;
            }
        }

        private void DeactivateLevelConditionCanvas(bool value)
        {
            levelConditionsCanvas.gameObject.SetActive(true);
            switch (value)
            {
                case true:
                    levelConditionsCanvas.transform.GetChild(0).gameObject.SetActive(false);
                    break;
                case false:
                    levelConditionsCanvas.transform.GetChild(1).gameObject.SetActive(false);
                    break;
            }
        }

        private void AssignLevelText(int levelTextValue)
        {
            levelTextLeftSide.text = levelTextValue.ToString();
            levelTextRightSide.text = (levelTextValue + 1).ToString();
        }

        private void IncreaseFillAmount()
        {
            var fillValue = (float) GameplayManager.Instance.AfterControlIncreasedCounterSize /
                            GameplayManager.Instance.LevelCollectableSize;

            _progressBarTween = progressBarImage.DOFillAmount(fillValue, .15f);
        }
    }
}