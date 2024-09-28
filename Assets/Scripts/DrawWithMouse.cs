using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class DrawWithMouse : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPos;
    [SerializeField] private float minDistance = 0.1f;
    private bool isDrawing = false;
    public SplineContainer splineContainer; // Reference to the spline container

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0; // Start with no points
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;

            Vector3 currentPos = GetMouseWorldPosition();
            Vector3 closestPointOnSpline = GetClosestPointOnSpline(currentPos);

            line.positionCount = 1;
            line.SetPosition(0, closestPointOnSpline);
            previousPos = closestPointOnSpline;
        }

        if (Input.GetMouseButton(0) && isDrawing)
        {
            Vector3 currentPos = GetMouseWorldPosition();
            Vector3 closestPointOnSpline = GetClosestPointOnSpline(currentPos);

            if (Vector3.Distance(closestPointOnSpline, previousPos) > minDistance)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, closestPointOnSpline);
                previousPos = closestPointOnSpline;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Assuming 2D
        return mousePos;
    }

    private Vector3 GetClosestPointOnSpline(Vector3 currentPos)
    {
        // Access the spline from the container
        Spline spline = splineContainer.Spline;

        float closestT = 0;
        float closestDistance = Mathf.Infinity;
        Vector3 closestPoint = Vector3.zero;

        // Iterate through a range of t values on the spline to find the closest point
        for (float t = 0; t <= 1; t += 0.01f) // This 0.01f step can be adjusted for precision
        {
            Vector3 splinePos = spline.EvaluatePosition(t); // Get position at 't' on spline
            float distance = Vector3.Distance(currentPos, splinePos);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = splinePos;
                closestT = t;
            }
        }

        return closestPoint;
    }
}
