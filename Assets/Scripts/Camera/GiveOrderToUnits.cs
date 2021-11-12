using System;
using System.Collections.Generic;
using UnitSpace;
using UnitSpace.Enums;
using UnitSpace.Orders;
using UnityEngine;

namespace PlayerCamera
{
    public class GiveOrderToUnits : MonoBehaviour
    {
        private Dictionary<UnitFraction, List<Unit>> _takedUnits;
        [SerializeField] private Unit _unitClone;
        [SerializeField] private Unit _enemyBaseClone;
        [SerializeField] private Unit _cannonClone;
        [SerializeField] private Unit _mineClone;
        [SerializeField] private UnitFraction _myFraction;
        [SerializeField] private UnitFraction _enemyFraction;
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
                foreach (var resource in _takedUnits[UnitFraction.Resources])
                    Unit.unitOrders.AddOrder(new PickUpResource(resource));
            }
        }
        public void FollowUnit()
        {
            foreach (var unit in _takedUnits[_myFraction])
            {
                var nearestUnit = unit.TakeNearest<Unit>(_takedUnits[_enemyFraction]);
                unit?.unitOrders.AddOrder(new FollowToOrder(nearestUnit));
            }  
        }
        public void PatrolUnit()
        {
            foreach (var unit in _takedUnits[_myFraction])
            {
                if (unit)
                    unit.unitOrders.AddOrder(new ModerateOrder(unit.transform.position, _enemyFraction));
            }
        }
        public void AttackUnit()
        {
            foreach (var unit in _takedUnits[_myFraction])
                unit?.unitOrders.AddOrder(new AttackOrder(_takedUnits[_enemyFraction]));
        }
        public void MineResource()
        {
            foreach (var unit in _takedUnits[_myFraction])
            {
                var nearest = unit.TakeNearest<ResourceMineContainer>(_takedUnits[UnitFraction.ResourceMine]);
                unit?.unitOrders.AddOrder(new MineResource(nearest));
            }
        }
        private void MoveToPoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.MaxValue))
                _takedUnits[_myFraction].ForEach(unit => unit.unitOrders.AddOrder(new MoveToOrder(hit.point)));
        }
        private void CreateUnit(Unit unit)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.MaxValue))
            {
                var unitClone = Instantiate(unit, hit.point + Vector3.up, Quaternion.identity);
                if(unitClone.fraction == _myFraction)
                    TakeUnit(unitClone, Color.green);
            }
        }
        private void Start()
        {
            _takedUnits = new Dictionary<UnitFraction, List<Unit>>();
            foreach (var type in Enum.GetValues(typeof(UnitFraction)))
                _takedUnits.Add((UnitFraction)type, new List<Unit>());
            TryGetComponent(out UnitTaker unitTakes);
            unitTakes.takeUnits.AddListener(TakeUnits);
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                MoveToPoint();
            if (Input.GetKeyDown(KeyCode.Space))
                CreateUnit(_unitClone);
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                foreach (var i in _takedUnits.Keys)
                    foreach (var j in _takedUnits[i])
                        Destroy(j.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.B))
                CreateUnit(_enemyBaseClone);
            if (Input.GetKeyDown(KeyCode.C))
                CreateUnit(_cannonClone);
            if (Input.GetKeyDown(KeyCode.M))
                CreateUnit(_mineClone);
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
            ActiveFractionUnitsSelectors(UnitFraction.Buildings, Color.green);
        }
        private void ActiveAllUnitsSelectors(Color color)
        {
            foreach (var key in _takedUnits.Keys)
                foreach (var unit in _takedUnits[key])
                    unit?.unitSelector.ChangeSelectorColor(color);
        }
        private void ActiveFractionUnitsSelectors(UnitFraction fraction, Color color)
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
