using System;
using System.Collections.Generic;
using System.Linq;
using UnitSpace.Enums;
using System.Threading.Tasks;

public class HpHealStatisticHandler : IDataStatisticHandler
{
    private List<int> _healings;
    private UnitType _unitEnemyType;
    public HpHealStatisticHandler(UnitType unitEnemyType)
    {
        _healings = new List<int>();
        _unitEnemyType = unitEnemyType;
    }
    public void WriteStatistic(UnitStatisticsData unitData)
    {
        _healings.Add((int)unitData.value);
    }
    public int GetTotalHealing()
        => _healings.Sum();
    public int GetSupressedDamage()
    {
        var damageHandler = RecordStatistics.Instance.GetService<DamageDealStatisticsHandler>(_unitEnemyType);
        return GetTotalHealing() - damageHandler.GetTotalDamagePerHit();
    }
    public void Clear()
    {
        _healings.Clear();
    }
    public override string ToString()
    => $"{GetType()}, total:{GetTotalHealing()} supressed {GetSupressedDamage()}";
}
