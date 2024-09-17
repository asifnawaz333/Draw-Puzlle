using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CircleMaker : MonoBehaviour
{
    public LineRenderer _circleRendered;
    [SerializeField] int steps;
    [SerializeField] int radius;
    // Start is called before the first frame update
    void Start()
    {
        DrawCirle(steps, radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void DrawCirle(int steps, float radius) // steps lines made cirles, radius size of cirle
    {
        _circleRendered.positionCount = steps; // how many lines are in a cricle means size
        
        // setuping a loop go through every steps
        for (int currentSteps = 0; currentSteps < steps; currentSteps++)
        {
            //current position of every step
            float circumferenceProgress = (float)currentSteps / steps; //"0.0-1"

            // getting current radian
            float currentRadian = circumferenceProgress *2*Mathf.PI; // 2*pi*circumference

            // getting sin  and cos
            float xScaled = Mathf.Cos(currentRadian); // left to right "0 - 1"
            float yScaled = Mathf.Sin(currentRadian); // top to bottom " 0-1"

            // now getting x and y values
            float x = xScaled*radius;
            float y = yScaled*radius;

            Vector3 currentPostion = new Vector3(x, y, 0);

            _circleRendered.SetPosition(currentSteps, currentPostion);

        }
    }
}
