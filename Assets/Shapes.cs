using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes : MonoBehaviour
{
    public LineRenderer _shapeRenders;
    [SerializeField] private int sides;
    [SerializeField] private float radius;
    [SerializeField] private int extraSteps =2;
    [SerializeField] private int width;


    public bool isLooped = true;
    // Start is called before the first frame update

    /*
        Loop is used in lineRenderer 
        if it's on we can contact starting and ending point
     */
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //_shapeRenders.SetWidth(width,width);
        if (isLooped)
        { DrawLooped(); }
        //else { DrawOverlapped(); }
    }

    void DrawLooped()
    {
        _shapeRenders.positionCount = sides;
        float TAU = 2 * Mathf.PI;  // 2pi

        for(int currentpoint = 0; currentpoint<sides; currentpoint++)
        {
            float currentRadian = ((float)currentpoint / sides*TAU);
            float x = Mathf.Cos(currentRadian) * radius;
            float y = Mathf.Sin(currentRadian) * radius;

            _shapeRenders.SetPosition(currentpoint, new Vector3(x, y, 0));
        }
        _shapeRenders.loop = true;
    }
    //void DrawOverlapped()
    //{
    //    DrawLooped();
    //    _shapeRenders.positionCount += extraSteps;
    //    int positionCount = _shapeRenders.positionCount;

    //    for(int i = 0; i < extraSteps; i++)
    //    {
    //        _shapeRenders.SetPosition(positionCount - extraSteps + i,_shapeRenders.GetPosition(i));
    //    }
    //}
}
