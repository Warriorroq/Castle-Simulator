using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviorExtensions
{   
    public static Transform TakeNearestInSpace(this Transform currentTrasform, IEnumerable<Transform> transforms)
    {
        var lastSqrtMagnitude = float.MaxValue;
        Transform nearestMono = null;
        foreach (var transform in transforms)
        {
            if (!transform)
                continue;

            var vectorDistance = currentTrasform.position - transform.transform.position;
            var distanceSqrtMagnitude = vectorDistance.sqrMagnitude;
            if (distanceSqrtMagnitude < lastSqrtMagnitude)
            {
                lastSqrtMagnitude = distanceSqrtMagnitude;
                nearestMono = transform;
            }
        }
        return nearestMono;
    }
}
