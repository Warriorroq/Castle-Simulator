using UnityEngine;
using UnitSpace;
using UnitSpace.Orders;
using UnitSpace.Fraction;
using UnityEngine.Events;
using UnitSpace.Interfaces;

public class OrderGiver : MonoBehaviour
{
    public UnityEvent<Unit> unitTake;
    private TakeUnit _unitTaker;
    [SerializeField] private Unit _lastTakedUnit;
    [SerializeField] private Unit _takedUnit;
    [SerializeField] private Unit _unitClone;
    [SerializeField] private ObjectFraction _myFraction;
    [SerializeField] private ObjectFraction _enemyFraction;
    public void ClearUnitOrders()
    {
        _takedUnit?.unitOrders.ClearOrders();
        _takedUnit?.unitOrders.StopOrder();
    }
    public void FollowUnit()
        =>GiveOrders(new FollowToOrder(_lastTakedUnit));
    public void PatrolUnit()
        =>GiveOrders(new ModerateOrder(_takedUnit.transform.position, _enemyFraction));
    public void AttackUnit()
        =>GiveOrders(new FollowToOrder(_lastTakedUnit), new AttackOrder(_lastTakedUnit));
    private void Awake()
    {
        _unitTaker = GetComponent<TakeUnit>();
        _unitTaker.takeUnit.AddListener(TakeUnit);
    }

    private void TakeUnit(Unit arg0)
    {
        if (!_lastTakedUnit)
            _lastTakedUnit = null;
        if (!_takedUnit)
            _takedUnit = null;
        _lastTakedUnit?.unitSelector.ChangeSelectorColor(Color.white);
        if (_lastTakedUnit == arg0)
        {
            _takedUnit?.unitSelector.ChangeSelectorColor(Color.white);
            _takedUnit = _lastTakedUnit;
            _takedUnit?.unitSelector.ChangeSelectorColor(Color.green);
            unitTake.Invoke(_takedUnit);
            _lastTakedUnit = null;
            return;
        }
        _lastTakedUnit = arg0;
        _lastTakedUnit?.unitSelector.ChangeSelectorColor(Color.red);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AddMovePoint();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateUnit();
        }
    }
    private void CreateUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, float.MaxValue))
        {
            Instantiate(_unitClone, hit.point + Vector3.up, Quaternion.identity);
        }
    }
    private void AddMovePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, float.MaxValue))
            GiveOrders(new MoveToOrder(hit.point));
    }
    private bool IsSameFraction(Unit unit)
        => unit.fraction == _myFraction;
    private void GiveOrders(params IOrder[] orders)
    {
        if (IsSameFraction(_takedUnit))
        {
            foreach (var order in orders)
                _takedUnit?.unitOrders.AddOrder(order);
        }
    }
}
