using System;
using System.Collections.Generic;
using UnitSpace;
using UnitSpace.Enums;
using UnitSpace.Orders;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace PlayerCamera
{
    public class GiveOrderToUnits : MonoBehaviour
    {
        public UnityEvent<List<Unit>, UnitType> takeUnits;
        private Dictionary<UnitType, List<Unit>> _takedUnits;
        [SerializeField] private Unit _unitClone;
        [SerializeField] private Unit _enemyBaseClone;
        [SerializeField] private Unit _cannonClone;
        [SerializeField] private Unit _mineClone;
        [SerializeField] private UnitType _myFraction;
        [SerializeField] private UnitType _enemyFraction;
        [SerializeField] private Vector3 mousePositionInWorld;
        public void DropResource()
            => _takedUnits[_myFraction].ForEach(unit => unit.resourcePosition.DropResource());
        public void RecoverOrder()
        {
            foreach (var unit in _takedUnits[_myFraction])
                unit?.unitOrders.AddOrder(new RecoverOrder());
        }
        public void ClearOrders()
        {
            foreach (var unit in _takedUnits[_myFraction])
            {
                unit?.unitOrders.StopOrder();
                unit?.unitOrders.ClearOrders();
            }
        }
        public void PickUpResources()
        {
            foreach(var Unit in _takedUnits[_myFraction])
            {
                foreach (var resource in _takedUnits[UnitType.Cargo])
                {
                    Unit.unitOrders.AddOrder(new PickUpResource(resource));
                }
            }
        }
        public void FollowUnit()
        {
            foreach (var unit in _takedUnits[_myFraction])
            {
                
                var nearestUnit = unit.transform.TakeNearestInSpace(_takedUnits[_enemyFraction].Select(x => x.transform));
                unit?.unitOrders.AddOrder(new FollowToOrder(nearestUnit));
            }  
        }
        public void PatrolUnit()
        {
            foreach (var unit in _takedUnits[_myFraction])
            {
                if (unit)
                {
                    unit.unitOrders.AddOrder(new ModerateOrder(mousePositionInWorld, _enemyFraction));
                }
            }
        }
        public void AttackUnit()
        {
            foreach (var unit in _takedUnits[_myFraction])
            {
                unit?.unitOrders.AddOrder(new AttackOrder(_takedUnits[_enemyFraction]));
            }
        }
        public void MineResource()
        {
            foreach (var unit in _takedUnits[_myFraction])
            {
                var nearestMine = unit.transform.TakeNearestInSpace(_takedUnits[UnitType.Mine].Select(x => x.transform));
                if (nearestMine is null)
                    return;
                var nearestSender = nearestMine.transform.TakeNearestInSpace(_takedUnits[UnitType.Buildings].Select(x => x.transform));
                var order = new MineResource(nearestMine.GetComponent<ResourceMineContainer>(), nearestSender.GetComponent<ResourceSender>());
                unit?.unitOrders.AddOrder(order);
            }
        }
        public void MoveToPoint()
        {
            _takedUnits[_myFraction].ForEach(unit => unit.unitOrders.AddOrder(new MoveToOrder(mousePositionInWorld)));
        }
        private void SetMousePointInWorld()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.MaxValue))
            {
                mousePositionInWorld = hit.point;
            }
        }
        private void CreateUnit(Unit unit)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.MaxValue))
            {
                Instantiate(ResourceEffects.SummonLightCircle, hit.point, Quaternion.AngleAxis(90, Vector3.left));
                var unitClone = Instantiate(unit, hit.point + Vector3.up, Quaternion.identity);
                if(unitClone.fraction == _myFraction)
                {
                    TakeUnit(unitClone, Color.green);
                    takeUnits.Invoke(_takedUnits[unitClone.fraction], unitClone.fraction);
                }
            }
        }
        private void Start()
        {
            var a = RecordStatistics.Instance;
            _takedUnits = new Dictionary<UnitType, List<Unit>>();
            foreach (UnitType type in Enum.GetValues(typeof(UnitType)))
                _takedUnits.Add(type, new List<Unit>());
            TryGetComponent(out UnitTaker unitTakes);
            unitTakes.takeUnits.AddListener(TakeUnits);
        }
        private void Update()
        {
            SetMousePointInWorld();
            if (Input.GetKeyDown(KeyCode.Space))
                CreateUnit(_unitClone);
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                foreach (var i in _takedUnits.Keys)
                    foreach (var j in _takedUnits[i])
                        if(j) Destroy(j.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.B))
                CreateUnit(_enemyBaseClone);
            if (Input.GetKeyDown(KeyCode.C))
                CreateUnit(_cannonClone);
            if (Input.GetKeyDown(KeyCode.M))
                CreateUnit(_mineClone);
            if (Input.GetKeyDown(KeyCode.R))
                RecordStatistics.Instance.StartRecording();
        }
        private void TakeUnit(Unit unit, Color selectorColor)
        {
            unit.unitSelector.ChangeSelectorColor(selectorColor);
            _takedUnits[unit.fraction].Add(unit);
        }
        private void TakeUnits(IEnumerable<Unit> arg0)
        {
            ActiveAllUnitsSelectors(Color.white);
            ClearDictionaryValues();
            foreach(var unit in arg0)
                _takedUnits[unit.fraction].Add(unit);
            ActiveAllUnitsSelectors(Color.red);
            ActiveFractionUnitsSelectors(_myFraction, Color.green);
            ActiveFractionUnitsSelectors(UnitType.Buildings, Color.green);
            foreach (UnitType type in Enum.GetValues(typeof(UnitType)))
                    takeUnits.Invoke(_takedUnits[type], type);
        }
        private void ActiveAllUnitsSelectors(Color color)
        {
            foreach (var key in _takedUnits.Keys)
                foreach (var unit in _takedUnits[key])
                    unit?.unitSelector.ChangeSelectorColor(color);
        }
        private void ActiveFractionUnitsSelectors(UnitType fraction, Color color)
        {
            foreach (var unit in _takedUnits[fraction])
                unit?.unitSelector.ChangeSelectorColor(color);
        }
        private void ClearDictionaryValues()
        {
            foreach (var key in _takedUnits)
                key.Value.Clear();
        }
        private string GetInfoAboutDictionary()
        {
            string str = string.Empty;
            foreach(var key in _takedUnits)
            {
                str += $"{key.Key}:\n";
                foreach(var value in key.Value)
                {
                    str += $"{value.name}, ";
                }
                str += "\n";
            }
            return str;
        }
    }
}
