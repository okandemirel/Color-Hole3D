using Managers;
using TMPro;
using UnityEngine;

namespace UIBehaviour
{
    public class UIInitialize : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private Canvas gameCanvas, levelConditionsCanvas;
        [SerializeField] private TextMeshProUGUI levelTextLeftSide, levelTextRightSide;

        #endregion

        private void Start()
        {
            UIManager.Instance.NewLevelUITextAssign = AssignLevelText;
            EventManager.Instance.LevelCanvasConditions = ActivateLevelConditionCanvas;
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

        private void ActivateGameCanvas()
        {
            gameCanvas.gameObject.SetActive(true);
        }

        private void AssignLevelText(int levelTextValue)
        {
            levelTextLeftSide.text = levelTextValue.ToString();
            levelTextRightSide.text = (levelTextValue + 1).ToString();
        }
    }
}