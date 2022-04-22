using UnityEngine;
using UnityEngine.Events;
using UnitSpace.Enums;
using System.Collections.Generic;

public class RecordStatistics : Singletone<RecordStatistics>, IDataStatisticHandler
{
    public ReadyState recordingState; 
    public UnityEvent startRecording;
    public UnityEvent stopRecording;
    [SerializeField] private float _recordingTime = 40;
    private Dictionary<int, Dictionary<int, IDataStatisticHandler>> _servicies;
    private void Awake()
    {
        _servicies = new Dictionary<int, Dictionary<int, IDataStatisticHandler>>();
        CreateServicies(UnitType.Core, UnitType.Void);
        CreateServicies(UnitType.Void, UnitType.Core);
    }
    public void StartRecording()
    {
        startRecording.Invoke();
        recordingState = ReadyState.Executing;
        Invoke(nameof(StopRecording), _recordingTime);
    }
    public void WriteStatistic(UnitStatisticsData unitData) {
        
        if(_servicies.ContainsKey((int)unitData.type))
            _servicies[(int)unitData.type][(int)unitData.action].WriteStatistic(unitData);
    }
    public TService GetService<TService>(UnitType unitType) where TService : IDataStatisticHandler
    {
        foreach(var serviceHandler in _servicies[(int)unitType].Values)
            if(serviceHandler.GetType() == typeof(TService))
                return (TService)serviceHandler;
        throw new System.Exception($"{GetType()}: Does not contain such service as {typeof(TService)}");
    }
    private void CreateServicies(UnitType unitFriendlyType, UnitType enemyType)
    {
        var dictionary = new Dictionary<int, IDataStatisticHandler>();
        _servicies.Add((int)unitFriendlyType, dictionary);
        dictionary.Add((int)action.DamageDealed, new DamageDealStatisticsHandler());
        dictionary.Add((int)action.Healed, new HpHealStatisticHandler(enemyType));
    }
    private void SaveStatistics()
    {
        foreach(var serviceHandler in _servicies[(int)UnitType.Core].Values)
            Debug.Log(serviceHandler.ToString());
    }
    private void StopRecording()
    {
        stopRecording.Invoke();
        recordingState = ReadyState.Stop;
        SaveStatistics();
        Clear();
    }
    public void Clear()
    {
        foreach(var unitTypeDictionary in _servicies.Keys)
            foreach (var statisticsHandler in _servicies[unitTypeDictionary].Values)
                statisticsHandler.Clear();
                
    }
    public enum action : int
    {
        DamageDealed = 0,
        Healed = 1,        
        LiveTime = 2,
    }
}
