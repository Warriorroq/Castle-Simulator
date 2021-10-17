using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Orders;
using UnitSpace;
using UnitSpace.Enums;

public class CreateEnemy : IOrder
{
    private Unit _owner;
    private Unit _prefab;
    private Unit _target;
    private IOrder.OrderState _state;
    private UnitFraction _enemyFraction;
    private UnitFraction _fraction;
    public CreateEnemy(Unit prefab, UnitFraction fraction, UnitFraction enemyFraction)
    {
        _prefab = prefab;
        _enemyFraction = enemyFraction;
        _fraction = fraction;
    }
    public void SetTarget(Unit unit)
        =>_target = unit;
    public void EndOrder()
    {
        _state = IOrder.OrderState.Finished;
    }

    public IOrder.OrderState GetState()
        => _state;

    public void SetUnitOwner(Unit owner)
    {
        _owner = owner;
        _state = IOrder.OrderState.Ready;
    }

    public void StartOrder()
    {
        _state = IOrder.OrderState.InProgress;
    }

    public void UpdateOrder()
    {
        SpawnUnits();
        EndOrder();
    }
    private void SpawnUnits()
    {
        var minion = Object.Instantiate(_prefab, _owner.transform.position + new Vector3(Random.Range(1,2), 0, Random.Range(-2, 2)), Quaternion.identity) as Unit;
        minion.fraction = _fraction;
        if(_target)
        {
            minion.unitOrders.AddOrder(new AttackOrder(_target));
        }
        minion.unitOrders.AddOrder(new ModerateOrder(_target.transform.position, _enemyFraction));
    }
}
