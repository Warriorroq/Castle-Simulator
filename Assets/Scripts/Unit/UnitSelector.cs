using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    private MeshRenderer _selector;
    public void ChangeSelectorColor(Color newColor)
        =>_selector.material.color = newColor;
    private void Awake()
    {
        TryGetComponent(out _selector);
    }
}
