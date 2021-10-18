using UnityEngine;
using UnitSpace;
using UnitSpace.Attributes;
using UnitSpace.Enums;
public class EnemyBase : MonoBehaviour
{
    [SerializeField] private Unit _prefab;
    [SerializeField] private UnitFraction _enemy;
    private void Start()
    {
        var unit = GetComponent<Unit>();
        unit.attributes.GetOrCreateAttribute<Health>().value = 100;
        unit.attributes.GetOrCreateAttribute<Health>().currentHp = 100;
        unit.attributes.GetOrCreateAttribute<Strenght>().value = 0;
        unit.attributes.GetOrCreateAttribute<Speed>().value = 0;
        unit.attributes.GetOrCreateAttribute<Sensitivity>().value = 30;
        InvokeRepeating(nameof(GiveCreateOrder), 1f, 2f);
    }
    private void GiveCreateOrder()
    {
        TryGetComponent(out Unit unit);
        unit.unitOrders.AddOrder(new CreateEnemy(_prefab, unit.fraction, _enemy));
    }
}
