using UnityEngine;
using UnitSpace;
using UnitSpace.Attributes;
using UnitSpace.Enums;
public class EnemyBase : MonoBehaviour
{
    [SerializeField] private Unit _prefab;
    private void Start()
    {
        var unit = GetComponent<Unit>();
        unit.attributes.GetOrCreateAttribute<Health>().value = 100;
        unit.attributes.GetOrCreateAttribute<Health>().currentHp = 100;
        unit.attributes.GetOrCreateAttribute<Strenght>().value = 0;
        unit.attributes.GetOrCreateAttribute<Speed>().value = 0;
        unit.attributes.GetOrCreateAttribute<Sensitivity>().value = 30;
        unit.unitOrders.AddOrder(new EnemyBaseLogic(_prefab, UnitFraction.Core));
    }
}
