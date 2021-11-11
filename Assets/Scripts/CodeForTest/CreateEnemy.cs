using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Orders;
using UnitSpace;
using UnitSpace.Enums;

public class CreateEnemy : Order
{
    private Unit _owner;
    private Unit _prefab;
    private UnitFraction _enemyFraction;
    private UnitFraction _fraction;
    public CreateEnemy(Unit prefab, UnitFraction fraction, UnitFraction enemyFraction)
    {
        _prefab = prefab;
        _enemyFraction = enemyFraction;
        _fraction = fraction;
    }
    public override void EndOrder()
    {
        base.EndOrder();
        SpawnUnits();
    }
    public override void StartOrder()
    {
        base.StartOrder();
        EndOrder();
    }
    public override void SetUnitOwner(Unit owner)
    {
        _owner = owner;
        base.SetUnitOwner(owner);
    }
    private void SpawnUnits()
    {
        var randomVector = new Vector3(Random.Range(1, 2), 1, Random.Range(-5, 5));
        var minion = Object.Instantiate(_prefab, _owner.transform.position + randomVector, Quaternion.identity) as Unit;
        minion.fraction = _fraction;
        minion.unitOrders.AddOrder(new ModerateOrder(_owner.transform.position, _enemyFraction, UnitFraction.Buildings));
        minion.GetComponent<MeshRenderer>().material.color = Color.black;
    }
}
