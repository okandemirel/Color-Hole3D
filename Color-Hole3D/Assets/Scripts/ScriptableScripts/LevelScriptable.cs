using UnityEngine;

namespace ScriptableScripts
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level ")]
    public class LevelScriptable : ScriptableObject
    {
        public GameObject LevelPrefab;
        public int LevelCollectableCount;

        public Color
            LowPolyGroundColor,
            GroundColor,
            FakeBordersColor,
            CollectableObjectsColor,
            NonCollectableObjectsColor;
    }
}