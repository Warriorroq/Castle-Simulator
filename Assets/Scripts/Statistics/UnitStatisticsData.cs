using UnitSpace.Enums;
public class UnitStatisticsData
{
    public RecordStatistics.action action;
    public UnitType type;
    public int value;
    public UnitStatisticsData(RecordStatistics.action action, UnitType type, int value)
    {
        this.action = action;
        this.type = type;
        this.value = value;
    }
}
