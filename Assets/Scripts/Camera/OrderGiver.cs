using UnityEngine;
using UnitSpace;
using UnitSpace.Orders;
public class OrderGiver : MonoBehaviour
{
    private TakeUnit _unitTaker;
    [SerializeField] private Unit _lastTakedUnit;
    [SerializeField] private Unit _takedUnit;
    private void Awake()
    {
        _unitTaker = GetComponent<TakeUnit>();
        _unitTaker.takeUnit.AddListener(TakeUnit);
    }

    private void TakeUnit(Unit arg0)
    {
        if (_lastTakedUnit == arg0)
            _takedUnit = _lastTakedUnit;
        _lastTakedUnit = arg0;        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(!Input.GetKey(KeyCode.LeftControl))
            {
                //_takedUnit.unitOrders.ClearOrders();
                //_takedUnit.unitOrders.StopOrder();
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (_lastTakedUnit != _takedUnit)
                    AttackUnit();
            }
            else if(Input.GetKey(KeyCode.F))
            {
                if (_lastTakedUnit != _takedUnit)
                    FollowUnit();
            }
            else
            {
                AddMovePoint();
            }
        }
    }
    private void FollowUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, float.MaxValue))
        {
            _takedUnit?.unitOrders.AddOrder(new FollowToOrder(_lastTakedUnit));
        }
    }
    private void AttackUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, float.MaxValue))
        {
            _takedUnit?.unitOrders.AddOrder(new FollowToOrder(_lastTakedUnit));
            _takedUnit?.unitOrders.AddOrder(new AttackOrder(_lastTakedUnit));
        }
    }
    private void AddMovePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, float.MaxValue))
        {
            _takedUnit?.unitOrders.AddOrder(new MoveToOrder(hit.point));
        }
    }
}
