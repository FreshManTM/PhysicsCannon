using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CannonProjection : MonoBehaviour
{
    
    [SerializeField] int _numPoints = 50;                // Number of points on the line
    [SerializeField] float _timeBetweenPoints = 0.1f;    // distance between points on the line

    LineRenderer _lineRenderer;
    CannonController _controller;
    
    float _gravity = - 9.81f;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _controller = GetComponent<CannonController>();
    }

    void Update()
    {
        _lineRenderer.positionCount = _numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = _controller.Muzzle.position;
        Vector3 startingVelocity = _controller.Muzzle.right * _controller.Power;

        for (float t = 0; t < _numPoints; t += _timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + _gravity / 2f * t * t;
            points.Add(newPoint);
        }

        _lineRenderer.SetPositions(points.ToArray());
    }
}
