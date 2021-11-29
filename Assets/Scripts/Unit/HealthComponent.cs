using UnitSpace;
using UnitSpace.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnitSpace.Enums;
public class HealthComponent : MonoBehaviour
{    
    //TODO: remove attack parts from it
    public UnityEvent<IteractData> giveIteractData;
    public UnityEvent<IteractData> takeIteractData;
    public UnityEvent destroyUnit;
    public bool HealthIsOverHealed => _health.IsOverHealed;
    [SerializeField] private float _reloadTime = 1;
    private Unit _owner;
    private Health _health;
    private ReadyState attackState;
    public bool IsReadyToAttack()
        => attackState == ReadyState.Ready;
    public bool CanUseStateAndReloadIteract()
    {
        if (attackState == ReadyState.Ready)
        {
            attackState = ReadyState.NonReady;
            Invoke(nameof(Reload), _reloadTime);
            return true;
        }
        return false;
    }
    public void GiveDamage(Unit target)
    {
        if (attackState == ReadyState.Ready)
        {
            attackState = ReadyState.NonReady;
            var data = new IteractData();
            Invoke(nameof(Reload), _reloadTime);
            giveIteractData?.Invoke(data);
            target.healthComponent.TakeDamage(data);
        }
    }
    public void GiveDamage(Unit target, IteractData iteractData)
    {
        if (attackState == ReadyState.Ready)
        {
            attackState = ReadyState.NonReady;
            Invoke(nameof(Reload), _reloadTime);
            giveIteractData?.Invoke(iteractData);
            target.healthComponent.TakeDamage(iteractData);
        }
    }
    public void HealthHeal(float Amount) { 
        _health.Heal(Amount); 
    }
    private void TakeDamageWithHealthAttribute(float Amount)
    {
        _health.currentHp -= Amount;
        if (_health.currentHp <= 0)
            DestroyThisUnit();
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
        InvokeRepeating(nameof(ReduceAddictiveHealth), 1f, 1f);
    }
    private void ReduceAddictiveHealth()
    {
        if (!HealthIsOverHealed)
            return;
        if (_health.currentHp > _health.value)
            _health.currentHp--;
        if (_health.currentHp < _health.value)
            _health.currentHp = _health.value;
    }
    private void DestroyThisUnit()
    {
        destroyUnit?.Invoke();
        _owner.skills.Dispose();
        Destroy(gameObject);
    }
    private void ReduceDamageFromHealth(IteractData data)
    {
        TakeDamageWithHealthAttribute(data.damage);
        Debug.Log($"{name} taked damage {data.damage} hp is {_health.currentHp}");
    }
    private void Reload()
        => attackState = ReadyState.Ready;
}
