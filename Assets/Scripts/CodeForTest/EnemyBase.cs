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
        InvokeRepeating(nameof(GiveCreateOrder), 1f, 2f);
    }
    private void GiveCreateOrder()
    {
        TryGetComponent(out Unit unit);
        unit.unitOrders.AddOrder(new CreateEnemy(_prefab, unit.fraction, _enemy));
    }
}
