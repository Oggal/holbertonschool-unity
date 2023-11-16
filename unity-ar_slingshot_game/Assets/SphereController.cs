using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{

    public Vector3 startPos;
    public Vector3 forwardDir;
    public Vector3 hitpos;
    public AnimationCurve curve;
    public AnimationCurve SpeedCurve;
    public bool isReady { get; internal set; } = true;

    public float throwTime = 0.0f;

    public void SetData(Vector3 startPos, Vector3 forwardDir, Vector3 hitpos)
    {
        this.startPos = startPos;
        this.forwardDir = forwardDir;
        this.hitpos = hitpos;
        transform.position = startPos;
    }

    public Vector3 CalcLine(float t)
    {
        return Vector3.Lerp(
            startPos + (forwardDir * t) + (Vector3.up * t * 2.0f),
            hitpos,
            curve.Evaluate(t));
    }
    public void Start()
    {

    }
    public void Update()
    {
        if(!isReady)
        {
            transform.position = CalcLine(SpeedCurve.Evaluate(throwTime));
            throwTime += Time.deltaTime;
            isReady = throwTime > 1.0f;
        }
        
    }

    public void launch()
    {
        isReady = false;
        throwTime = 0.0f;
    }
}
