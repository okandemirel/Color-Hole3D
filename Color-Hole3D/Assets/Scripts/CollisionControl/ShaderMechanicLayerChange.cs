using UnityEngine;

namespace CollisionControl
{
    public class ShaderMechanicLayerChange : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private int layerOnEnter; // BallInHole
        [SerializeField] private int layerOnExit; // BallOnTable

        #endregion


        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Collectable") || other.CompareTag("Uncollectable"))
            {
                other.isTrigger = true;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Collectable") || other.CompareTag("Uncollectable"))
            {
                //
            }
        }
    }
}