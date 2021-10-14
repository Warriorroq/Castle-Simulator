using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateUnitFractionImage : MonoBehaviour
{
    private Canvas _canvas;
    private TakeUnit _takeUnit;
    private OrderGiver _orderGiver;
    private List<Image> _images;
    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
        TryGetComponent(out _takeUnit);
        TryGetComponent(out _orderGiver);
    }
}
