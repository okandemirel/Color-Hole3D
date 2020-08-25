using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIBehaviour
{
    public class LevelFailedScriptEnable : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private Image circleCountDownImage;
        [SerializeField] private GameObject noThanksText;
        [SerializeField] private Button continueButton;

        #endregion

        private void OnEnable()
        {
            if (!GameplayManager.Instance.AdvertisementPlayed) StartCountDown();
            else
            {
                circleCountDownImage.transform.parent.gameObject.SetActive(false);
                noThanksText.gameObject.SetActive(false);
                continueButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Restart";
                continueButton.onClick.RemoveAllListeners();
                continueButton.onClick.AddListener(ContinueButton.Restart);
            }
        }

        private void StartCountDown()
        {
            circleCountDownImage.gameObject.SetActive(true);
            circleCountDownImage.DOFillAmount(1, 5)
                .SetEase(Ease.Linear)
                .OnComplete(() => noThanksText.SetActive(true));
        }
    }
}