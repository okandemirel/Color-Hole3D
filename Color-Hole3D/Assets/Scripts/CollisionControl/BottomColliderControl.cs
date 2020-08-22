using System;
using Managers;
using UnityEngine;

namespace CollisionControl
{
    public class BottomColliderControl : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                Debug.Log("Collided with a collectable");
                iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
                GameplayManager.Instance.IncreaseCounterAndControl();
            }

            if (other.CompareTag("Uncollectable"))
            {
                Debug.Log("Collided with a uncollectable");
                iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
                EventManager.Instance.LevelCanvasConditions.Invoke(false);
                EventManager.Instance.LevelTouchConditions.Invoke(false);
                CameraManager.Instance.ShakeCamera();
            }
        }
    }
}