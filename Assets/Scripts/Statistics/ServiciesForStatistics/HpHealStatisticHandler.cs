using System;
using System.Collections.Generic;
using System.Linq;
using UnitSpace.Enums;
using System.Threading.Tasks;

public class HpHealStatisticHandler : IDataStatisticHandler
{
    private List<float> _healings;
    private UnitType _unitEnemyType;
    public HpHealStatisticHandler(UnitType unitEnemyType)
    {
        _healings = new List<float>();
        _unitEnemyType = unitEnemyType;
    }
    public void WriteStatistic(UnitStatisticsData unitData)
    {
        _healings.Add((float)unitData.value);
    }
    public float GetTotalHealing()
        => _healings.Sum();
    public float GetNonSupressedDamage()
    {
        var damageHandler = RecordStatistics.Instance.GetService<DamageDealStatisticsHandler>(_unitEnemyType);
        return GetTotalHealing() - damageHandler.GetTotalDamagePerHit();
    }
    public void Clear()
    {
        _healings.Clear();
    }
    public override string ToString()
    => $"{GetType()}, total:{GetTotalHealing()} supressed {GetNonSupressedDamage()}";
}
