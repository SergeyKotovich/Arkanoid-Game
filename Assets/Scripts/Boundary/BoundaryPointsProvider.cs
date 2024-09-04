using System;
using UnityEngine;

[Serializable]
public class BoundaryPointsProvider
{
    [SerializeField] private Transform _leftBoundary;
    [SerializeField] private Transform _rightBoundary;

    public Transform LeftBoundary => _leftBoundary;
    public Transform RightBoundary => _rightBoundary;
}