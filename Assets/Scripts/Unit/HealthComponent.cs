using UnitSpace;
using UnitSpace.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnitSpace.Enums;
public class HealthComponent : MonoBehaviour
{    
    public UnityEvent<IteractData> giveIteractData;
    public UnityEvent<IteractData> takeIteractData;
    public UnityEvent destroyUnit;
    private Unit _owner;
    private Health _health;
    private ReadyState attackState;
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
    public bool IsReadyToAttack()
        => attackState == ReadyState.Ready;
    public void GiveDamage(Unit target)
    {
        attackState = ReadyState.NonReady;
        Invoke(nameof(Reload), 1f);
        var data = new IteractData();
        giveIteractData?.Invoke(data);
        target.healthComponent.TakeDamage(data);
    }
    private void TakeDamage(IteractData data)
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
        attackState = ReadyState.Ready;
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
        Debug.Log($"{name} taked damage {data.damage} hp is {Health}");
    }
    private void Reload()
        => attackState = ReadyState.Ready;
}
