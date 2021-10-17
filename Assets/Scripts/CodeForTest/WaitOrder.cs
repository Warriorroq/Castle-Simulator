using System.Collections;
using System.Collections.Generic;
using UnitSpace;
using UnitSpace.Interfaces;
using UnityEngine;

public class WaitOrder : IOrder
{
    private IOrder.OrderState _state;
    private float _timer;
    private float time;
    public WaitOrder(float seconds)
    {
        time = seconds;
    }
    public void EndOrder()
    {       
        _state = IOrder.OrderState.Finished;
    }

    public IOrder.OrderState GetState()
        => _state;

    public void SetUnitOwner(Unit owner)
    {
        _state = IOrder.OrderState.Ready;
    }

    public void StartOrder()
    {
        _state = IOrder.OrderState.InProgress;
    }

    public void UpdateOrder()
    {
        if(_timer > time)
            EndOrder();
        _timer += Time.deltaTime;
    }
}
