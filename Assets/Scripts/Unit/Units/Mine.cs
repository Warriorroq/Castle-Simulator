using System.Collections.Generic;
using UnitSpace.Enums;
using UnityEngine;

namespace UnitSpace
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private Unit _owner;
        [SerializeField] private List<UnitType> _triggerFractions;
        private void Awake()
        {
            _owner = GetComponent<Unit>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent<Unit>(out var target))
            {
                if(_triggerFractions.Contains(target.fraction))
                {
                    _owner.healthComponent.GiveDamage(target);
                    Destroy(gameObject);
                }
            }
        }
    }
}
