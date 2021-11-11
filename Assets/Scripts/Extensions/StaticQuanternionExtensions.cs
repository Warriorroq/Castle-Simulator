using UnityEngine;

public static class StaticQuanternionExtensions
{
    public static float DegreeToApproximatelyThirdValue(this Quaternion quatA, float value)
        => value * 0.0000004f;
    public static bool Approximately(this Quaternion quatA, Quaternion value, float acceptableRange)
        => Mathf.Abs(Quaternion.Dot(quatA, value)) >= 1 - acceptableRange;
}
