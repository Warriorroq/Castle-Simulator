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
    private ReadyState _reloadState;
    private UnitFraction _enemyFraction;
    private UnitFraction _baseFraction;
    private Sensitivity _sensitivity;
    private float _timer;

    public EnemyBaseLogic(Unit prefab, UnitFraction fraction, UnitFraction enemyFraction)
    {
        _reloadState = ReadyState.Ready;
        _prefab = prefab;
        _enemyFraction = enemyFraction;
        _baseFraction = fraction;
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
        _orderState = IOrder.OrderState.Ready;
        _sensitivity = _owner.attributes.GetOrCreateAttribute<Sensitivity>();
    }

    public void StartOrder()
    {
        _orderState = IOrder.OrderState.InProgress;
        Debug.Log(_timer);
    }

    public void UpdateOrder()
    {
        if (_reloadState == ReadyState.Ready)
        {
            FindTarget();
        }
        if (_timer > 2f)
        {
            Reload();
            _timer = 0;
        }
        if(_reloadState == ReadyState.NonReady)
            _timer += Time.deltaTime;
    }
    private void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_owner.transform.position, _sensitivity.value);
        foreach (var hit in hitColliders)
        {
            if (hit.TryGetComponent<Unit>(out var target) && TryTakeTarget(target))
            {
                var order = new CreateEnemy(_prefab, _baseFraction, _enemyFraction);
                order.SetTarget(target);
                _owner.unitOrders.AddOrder(order);
                _reloadState = ReadyState.NonReady;
                EndOrder();
            }
        }
    }
    private bool TryTakeTarget(Unit target)
        => target != _owner && target.fraction == _enemyFraction;
    private void Reload()
        => _reloadState = ReadyState.Ready;
}
