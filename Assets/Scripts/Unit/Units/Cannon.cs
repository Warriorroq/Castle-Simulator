using UnityEngine;
using UnitSpace.Attributes;
using System.Collections.Generic;
using UnitSpace.Enums;

namespace UnitSpace
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] private CannonHead _head;
        [SerializeField] private Unit _target;
        [SerializeField] private Unit _owner;
        [SerializeField] private List<UnitType> _enemyFraction;
        [SerializeField] private Quaternion lastRotation;
        [SerializeField] private Sensitivity _sensitivity;
        private void Awake()
        {
            _owner = GetComponent<Unit>();
            _head = GetComponentInChildren<CannonHead>();
        }
        private void Start()
        {
            _sensitivity = _owner.attributes.GetOrCreateAttribute<Sensitivity>();
        }
        private void Update()
        {
            if (!_target)
            {
                FindTarget();
                return;
            }
            var distance = transform.position - _target.transform.position;
            if(distance.sqrMagnitude > _sensitivity.value * _sensitivity.value)
            {
                _target = null;
            }
            if (_target is null)
                return;
            lastRotation = _head.transform.rotation;
            _head.transform.LookAt(_target.transform.position);
            if (_head.transform.rotation.Approximately(lastRotation, lastRotation.DegreeToApproximatelyThirdValue(20)))
                _owner.healthComponent.GiveDamage(_target);
            _head.transform.rotation = Quaternion.Lerp(lastRotation, _head.transform.rotation, 7f * Time.deltaTime);
        }
        private void FindTarget()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _sensitivity.value);
            foreach (var hit in hitColliders)
            {
                if (hit.TryGetComponent<Unit>(out var target) && TargetIsNormal(target))
                {
                    _target = target;
                    return;
                }
            }
        }
        private bool TargetIsNormal(Unit target)
            => target != this && _enemyFraction.Contains(target.fraction);
    }
}
