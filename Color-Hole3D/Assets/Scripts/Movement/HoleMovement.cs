using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Movement
{
    public class HoleMovement : MonoBehaviour
    {
        #region Public Variables

        [Header("Hole Movement Controls")] public int HoleMovementSpeed;

        #endregion

        #region Serialized Variables

        [Space] [Header("Hole Variables")] [SerializeField]
        private MeshFilter holeMeshFilter;

        [SerializeField] private MeshCollider holeMeshCollider;
        [SerializeField] private Transform holeCenter;
        [SerializeField] private float radius;
        [SerializeField] private Vector2 moveLimits;
        [SerializeField] private List<int> _holeVertices = new List<int>();

        #endregion

        #region Private Variables

        private Mesh _holeMesh;

        private readonly List<Vector3> _offsets = new List<Vector3>();
        private int _holeVerticesCount;
        private Vector3 _movePos, _afterLimitsMovePos;

        #endregion

        private void Start()
        {
            _holeMesh = holeMeshFilter.mesh;

            FindHoleVertices();
        }

        private void Update()
        {
            if (!TouchManager.Instance.IsTouching) return;

            MoveHole();

            UpdateVertices();
        }

        private void MoveHole()
        {
            var position = holeCenter.position;

            _movePos = Vector3.Lerp(position,
                position + TouchManager.Instance.TouchPositionForMovement,
                HoleMovementSpeed * Time.deltaTime);

            _afterLimitsMovePos = new Vector3(Mathf.Clamp(_movePos.x, -moveLimits.x, moveLimits.x),
                _movePos.y,
                Mathf.Clamp(_movePos.z, -moveLimits.y, moveLimits.y + .53f));

            holeCenter.position = _afterLimitsMovePos;
        }

        private void UpdateVertices()
        {
            //Change vertices values
            var vertices = _holeMesh.vertices;
            for (var i = 0; i < _holeVerticesCount; i++)
            {
                vertices[_holeVertices[i]] = holeCenter.position + _offsets[i];
            }

            //Update Mesh 
            _holeMesh.vertices = vertices;
            holeMeshFilter.mesh = _holeMesh;
            holeMeshCollider.sharedMesh = _holeMesh;
        }

        private void FindHoleVertices()
        {
            for (var i = 0; i < _holeMesh.vertexCount; i++)
            {
                var distance = Vector3.Distance(holeCenter.position, _holeMesh.vertices[i]);

                if (distance < radius)
                {
                    _holeVertices.Add(i);
                    _offsets.Add(_holeMesh.vertices[i] - holeCenter.position);
                }

                _holeVerticesCount = _holeVertices.Count;
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(holeCenter.position, radius);
        }
    }
}