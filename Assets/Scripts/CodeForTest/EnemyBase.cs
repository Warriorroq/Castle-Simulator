using UnityEngine;
using UnitSpace.Orders;
using UnitSpace.Enums;
namespace UnitSpace
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private Unit _prefab;
        [SerializeField] private UnitFraction _enemy;
        private void Start()
        {
            var unit = GetComponent<Unit>();
            InvokeRepeating(nameof(GiveCreateOrder), 1f, 1.22f);
        }
        private void GiveCreateOrder()
        {
            TryGetComponent(out Unit unit);
            unit?.unitOrders.AddOrder(new CreateEnemy(_prefab, unit.fraction, _enemy));
        }
    }
}
