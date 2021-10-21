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
        [SerializeField] private UnitFraction _myFraction;
        [SerializeField] private UnitFraction _enemyFraction;
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
                foreach (var enemy in _takedUnits[_enemyFraction])
                    unit?.unitOrders.AddOrder(new FollowToOrder(enemy));
            //To DO: follow nearest enemy;
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
        private void MoveToPoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.MaxValue))
                _takedUnits[_myFraction].ForEach(unit => unit.unitOrders.AddOrder(new MoveToOrder(hit.point)));
        }
        private void CreateUnit()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.MaxValue))
            {
                Instantiate(_unitClone, hit.point + Vector3.up, Quaternion.identity);
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
                CreateUnit();
        }

        private void TakeUnits(List<Unit> arg0)
        {
            ActiveAllUnitsSelectors(Color.white);
            ClearDictionaryValues();
            foreach(var unit in arg0)
                _takedUnits[unit.fraction].Add(unit);
            ActiveAllUnitsSelectors(Color.red);
            ActiveFractionUnitsSelectors(_myFraction, Color.green);
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
