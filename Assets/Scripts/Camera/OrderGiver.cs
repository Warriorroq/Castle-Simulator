using UnityEngine;
using UnitSpace;
using UnitSpace.Orders;
public class OrderGiver : MonoBehaviour
{
    private TakeUnit _unitTaker;
    [SerializeField] private Unit tackedUnit;
    private void Awake()
    {
        _unitTaker = GetComponent<TakeUnit>();
        _unitTaker.takeUnit.AddListener(TakeUnit);
    }

    private void TakeUnit(Unit arg0)
        => tackedUnit = arg0;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            AddMovePoint();
    }
    private void AddMovePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, float.MaxValue))
        {
            tackedUnit?.unitOrders.AddOrder(new MoveToOrder(hit.point));
        }
    }
}
