using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class SplineLineRenderer : MonoBehaviour
{
    public SplineContainer splineContainer;  // Reference to the Spline
    public SplineExtrude splineExtrude;      // Reference to the SplineExtrude
    public float drawSpeed = 0.1f;           // Speed to move along the spline path
    private LineRenderer lineRenderer;
    private float t = 0; // T parameter to traverse along the spline

    private Vector2 previousTouchPos;        // Store the previous finger/mouse position
    private bool isTouching = false;         // To track if touch/mouse is held

    private void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;  // Start with 0 points
    }

    private void Update()
    {
        // Detect touch on mobile or mouse click on PC
        if (Input.touchCount > 0 || Mouse.current.leftButton.isPressed)
        {
            // For mobile touch input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == UnityEngine.TouchPhase.Began)
                {
                    previousTouchPos = touchPos;
                    isTouching = true;
                }

                if (touch.phase == UnityEngine.TouchPhase.Moved && isTouching)
                {
                    // Check the direction of the finger movement
                    if (touchPos.x > previousTouchPos.x)  // Moving right
                    {
                        DrawPathOnSpline(touchPos);
                    }
                    previousTouchPos = touchPos; // Update the previous position
                }

                if (touch.phase == UnityEngine.TouchPhase.Ended)
                {
                    isTouching = false;
                }
            }
            // For mouse input
            else if (Mouse.current.leftButton.isPressed)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

                if (!isTouching)
                {
                    previousTouchPos = mousePos;
                    isTouching = true;
                }

                // Check if moving right
                if (mousePos.x > previousTouchPos.x)
                {
                    DrawPathOnSpline(mousePos);
                }

                previousTouchPos = mousePos;  // Update the previous position

                if (!Mouse.current.leftButton.isPressed)
                {
                    isTouching = false;
                }
            }
        }
    }

    private void DrawPathOnSpline(Vector2 fingerPos)
    {
        // Get closest point on the spline based on the current t value
        Spline spline = splineContainer.Spline;

        if (spline == null) return;

        // Move along the spline by incrementing 't' based on drawSpeed
        t += drawSpeed * Time.deltaTime;

        if (t > 1f)
            t = 1f;  // Ensure t does not exceed 1

        // Get the relative distance along the spline path
        float relativeDistance = 0f;

        // Get the position on the spline using normalized 't'
        Vector3 pointOnSpline = SplineUtility.GetPointAtLinearDistance(spline, t, 0f, out relativeDistance);

        // Draw a line as the player moves their finger/mouse
        if (lineRenderer.positionCount == 0)
        {
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, pointOnSpline);
        }

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pointOnSpline);

        // Move line along the extrusion path if `SplineExtrude` is used
        if (splineExtrude != null)
        {
            Vector3 extrudedPoint = splineExtrude.transform.TransformPoint(pointOnSpline);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, extrudedPoint);
        }
    }
}
