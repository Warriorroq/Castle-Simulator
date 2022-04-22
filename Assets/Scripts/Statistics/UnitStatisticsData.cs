using UnitSpace.Enums;
public class UnitStatisticsData
{
    public RecordStatistics.action action;
    public UnitType type;
    public object value;
    public UnitStatisticsData(RecordStatistics.action action, UnitType type, object value)
    {
        this.action = action;
        this.type = type;
        this.value = value;
    }
}
