using System.Collections;
using System.Collections.Generic;
using UnitSpace;
using UnitSpace.Attributes;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{    
    public UnityEvent<IteractData> giveIteractData;
    public UnityEvent<IteractData> takeIteractData;
    public UnityEvent destroyUnit;
    private Unit _owner;
    private Health _health;
    private float Health
    {
        get
            => _health.currentHp;
        set
        {
            if (value <= 0)
                DestroyThisUnit();
            _health.currentHp = value;
        }
    }
    public void TakeDamage(IteractData data)
    {
        takeIteractData?.Invoke(data);
        ReduceDamageFromHealth(data);
    }
    private void Awake()
    {
        TryGetComponent(out _owner);
    }
    private void Start()
    {
        _health = _owner.attributes.GetOrCreateAttribute<Health>();
    }
    private void DestroyThisUnit()
    {
        destroyUnit?.Invoke();
        _owner.skills.Dispose();
        Destroy(gameObject);
    }
    private void ReduceDamageFromHealth(IteractData data)
    {
        Health -= data.damage;
        Debug.Log($"{name} tacked damage {data.damage} hp is {Health}");
    }
}
