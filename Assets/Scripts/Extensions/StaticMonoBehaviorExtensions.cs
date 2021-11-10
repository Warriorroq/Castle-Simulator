using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticMonoBehaviorExtensions
{
    
    public static T TakeNearest<T>(this MonoBehaviour gameObject, IEnumerable<MonoBehaviour> targets) 
        where T : MonoBehaviour
    {
        var minMagnitude = float.MaxValue;
        MonoBehaviour nearestMono = null;
        foreach (var target in targets)
        {
            if (!target)
                continue;
            var distance = gameObject.transform.position - target.transform.position;
            var distanceMagnitude = distance.sqrMagnitude;
            if (distanceMagnitude < minMagnitude)
            {
                minMagnitude = distanceMagnitude;
                nearestMono = target;
            }
        }
        return nearestMono?.GetComponent<T>();
    }
}
