using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitSpace
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private Unit _owner;
        private void Awake()
        {
            _owner = GetComponent<Unit>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent<Unit>(out var target))
            {
                _owner.healthComponent.GiveDamage(target);
                Destroy(gameObject);
            }
        }
    }
}
