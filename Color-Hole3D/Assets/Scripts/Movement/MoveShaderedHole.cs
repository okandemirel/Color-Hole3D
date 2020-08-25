using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MoveShaderedHole : MonoBehaviour
{
    #region Public Variables

    [Header("Hole Movement Controls")] public int HoleMovementSpeed;
    public Vector2 MoveLimits;
    public bool AfterZoneChangeMovementLimits;

    #endregion

    #region Serialized Variables

    [Space] [Header("Hole Variables")] [SerializeField]
    private Transform holeCenter;

    #endregion

    #region Private Variables

    private Vector3 _movePos, _afterLimitsMovePos;

    #endregion

    private void Start()
    {
#if UNITY_EDITOR
        HoleMovementSpeed = 6;
#else
        HoleMovementSpeed = 2;
#endif
    }

    private void Update()
    {
        if (!TouchManager.Instance.IsTouching) return;

        MoveHole();
    }

    private void MoveHole()
    {
        var position = holeCenter.position;

        _movePos = Vector3.Lerp(position,
            position + TouchManager.Instance.TouchPositionForMovement,
            HoleMovementSpeed * Time.deltaTime);

        if (!AfterZoneChangeMovementLimits)
        {
            _afterLimitsMovePos = new Vector3(Mathf.Clamp(_movePos.x, -MoveLimits.x, MoveLimits.x),
                _movePos.y,
                Mathf.Clamp(_movePos.z, -MoveLimits.y, MoveLimits.y + .09f));
        }
        else
        {
            _afterLimitsMovePos = new Vector3(Mathf.Clamp(_movePos.x, -MoveLimits.x, MoveLimits.x),
                _movePos.y,
                Mathf.Clamp(_movePos.z, -MoveLimits.y + 1.72f, MoveLimits.y + 1.8f));
        }

        holeCenter.position = _afterLimitsMovePos;
    }
}