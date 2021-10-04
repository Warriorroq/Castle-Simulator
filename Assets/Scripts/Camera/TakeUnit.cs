using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnitSpace;
public class TakeUnit : MonoBehaviour
{
    public UnityEvent<Unit> takeUnit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.MaxValue))
            {
                if (hit.collider.TryGetComponent<Unit>(out var newUnit))
                    takeUnit.Invoke(newUnit);
            }
        }
    }
}
