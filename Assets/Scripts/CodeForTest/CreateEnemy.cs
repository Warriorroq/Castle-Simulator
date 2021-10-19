using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Orders;
using UnitSpace;
using UnitSpace.Enums;

public class CreateEnemy : IOrder
{
    private Unit _owner;
    private Unit _prefab;
    private IOrder.OrderState _state;
    private UnitFraction _enemyFraction;
    private UnitFraction _fraction;
    public CreateEnemy(Unit prefab, UnitFraction fraction, UnitFraction enemyFraction)
    {
        _prefab = prefab;
        _enemyFraction = enemyFraction;
        _fraction = fraction;
    }
    public void EndOrder()
    {
        _state = IOrder.OrderState.Finished;
        SpawnUnits();
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
        EndOrder();
    }
    private void SpawnUnits()
    {
        var randomVector = new Vector3(Random.Range(1, 2), 1, Random.Range(-5, 5));
        var minion = Object.Instantiate(_prefab, _owner.transform.position + randomVector, Quaternion.identity) as Unit;
        minion.fraction = _fraction;
        minion.unitOrders.AddOrder(new ModerateOrder(_owner.transform.position, _enemyFraction));
        minion.GetComponent<MeshRenderer>().material.color = Color.black;
    }
}
