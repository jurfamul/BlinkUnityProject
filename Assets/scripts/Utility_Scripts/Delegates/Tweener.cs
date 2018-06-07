using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour  {

    public delegate Vector3 Tween(Vector3 start, Vector3 end, float t);
    public enum TweenType { Linear, Quad, Cubic, Fourth, Quint }

    protected static Vector3 LinearTween(Vector3 start, Vector3 end, float t)
    {
        float tt = Mathf.Clamp01(t);
        return start + tt * (end - start);
    }

    protected static Vector3 QuadTween(Vector3 start, Vector3 end, float t)
    {
        float tt = Mathf.Clamp01(t);
        // go to half, then slow down
        if (t < .5f)
        {
            return start + 8.0f * tt * tt * tt * tt * (end - start);
        }
        else
        {
            float rev = 1.0f - tt;
            return end - 8.0f * (rev * rev * rev * rev * (end - start));
        }
    }

    protected static Vector3 CubicTween(Vector3 start, Vector3 end, float t)
    {
        float tt = Mathf.Clamp01(t);
        if (t < 0.33334f)
        {
            return start + 8.0f * tt * tt * tt * (end - start);
        }
        else if (t < 0.666667f)
        {
            float rev = 1.0f - tt;
            return end - 8.0f * (rev * rev * rev * (end - start));
        }
        else
        {
            return end - 8.0f * tt * tt * tt * (end - start);
        }
    }

    protected static Vector3 FourthTween(Vector3 start, Vector3 end, float t)
    {
        return start;
    }

    protected static Vector3 QuintTween(Vector3 start, Vector3 end, float t)
    {
        return start;
    }

    public static Tween MakeTween(TweenType type)
    {
        switch (type)
        {
            case TweenType.Linear:
                return LinearTween;
            case TweenType.Quad:
                return QuadTween;
            case TweenType.Cubic:
                return CubicTween;
            case TweenType.Fourth:
                return FourthTween;
            case TweenType.Quint:
                return QuintTween;
        }
        return null;
    }

}
