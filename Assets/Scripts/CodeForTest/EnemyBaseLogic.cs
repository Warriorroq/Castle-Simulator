using UnitSpace.Interfaces;
using UnitSpace;
using UnitSpace.Enums;
using UnitSpace.Attributes;
using UnityEngine;
public class EnemyBaseLogic : IOrder
{
    private Unit _owner;
    private Unit _prefab;
    private IOrder.OrderState _orderState;
    private UnitFraction _enemyFraction;
    private Sensitivity _sensitivity;

    public EnemyBaseLogic(Unit prefab, UnitFraction enemyFraction)
    {
        _prefab = prefab;
        _enemyFraction = enemyFraction;
        _orderState = IOrder.OrderState.Ready;
    }
    public void EndOrder()
    {
        _orderState = IOrder.OrderState.Finished;
        _owner.unitOrders.AddOrder(this);
    }

    public IOrder.OrderState GetState()
        => _orderState;

    public void SetUnitOwner(Unit owner)
    {
        _owner = owner;
        _sensitivity = _owner.attributes.GetOrCreateAttribute<Sensitivity>();
    }

    public void StartOrder()
    {
        _orderState = IOrder.OrderState.InProgress;
    }

    public void UpdateOrder()
    {
        FindTarget();
    }
    private void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_owner.transform.position, _sensitivity.value);
        foreach (var hit in hitColliders)
        {
            if (hit.TryGetComponent<Unit>(out var target) && target.fraction == _enemyFraction)
            {
                var order = new CreateEnemy(_prefab, _owner.fraction, _enemyFraction);
                order.SetTarget(target);
                _owner.unitOrders.AddOrder(order);
                _owner.unitOrders.AddOrder(new WaitOrder(2f));
                _owner.unitOrders.AddOrder(this);
                EndOrder();
            }
        }
    }
}
