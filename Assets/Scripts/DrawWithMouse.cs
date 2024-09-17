using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPos;
    [SerializeField] private float minDistance = 0.1f;
    private bool isDrawing = false; // To track if we're currently drawing

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0; // Start with no points
    }

    private void Update()
    {
        // Start drawing when mouse or touch is first pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Start drawing
            isDrawing = true;

            // Get the initial position and add the first point
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPos.z = 0;

            line.positionCount = 1;
            line.SetPosition(0, currentPos);
            previousPos = currentPos;
        }

        // Continue drawing while holding the mouse button down
        if (Input.GetMouseButton(0) && isDrawing)
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPos.z = 0;

            // Add a new point if the distance is greater than minDistance
            if (Vector3.Distance(currentPos, previousPos) > minDistance)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, currentPos);
                previousPos = currentPos;
            }
        }

        // Stop drawing when mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
    }
}
