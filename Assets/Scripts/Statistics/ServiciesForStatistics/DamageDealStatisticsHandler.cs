using System;
using System.Collections.Generic;
using System.Linq;

public class DamageDealStatisticsHandler : IDataStatisticHandler
{
    private List<int> _givenDamage;
    public DamageDealStatisticsHandler()
    {
        _givenDamage = new List<int>();
    }
    public void WriteStatistic(UnitStatisticsData unitData) {
        _givenDamage.Add((int)unitData.value);
    }
    public int GetTotalDamagePerHit()
        => _givenDamage.Sum();
    public int GetAverageDamagePerHit()
    {
        var devider = _givenDamage.Count();
        if(devider == 0)
            devider = 1;
        return GetTotalDamagePerHit() / devider;
    }
    public void Clear()
        => _givenDamage.Clear();
    public override string ToString()
    => $"{GetType()}, total:{GetTotalDamagePerHit()}, average {GetAverageDamagePerHit()}";

    public object GetStatisticData()
    {
        throw new NotImplementedException();
    }
}
