using System.Collections;
using System.Collections.Generic;
using UnitSpace;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoReader : MonoBehaviour
{
    [SerializeField] private Text _unitName;
    [SerializeField] private Text _unitInfo;
    [SerializeField] private OrderGiver _takeUnit;
    [SerializeField] private CreateSkillButtons _createSkillButtons;
    public void ClearInformation()
    {
        _unitName.text = string.Empty;
        _unitInfo.text = string.Empty;
    }
    public void UpdateUnitInformation(Unit unit)
    {
        _createSkillButtons.Create(unit);
        LevelingUpByAttributes.GetInstance().CountUnitTotalExp(unit, out var exp);
        _unitName.text = unit.name;
        _unitInfo.text = $"attributes: \n {unit.attributes} \n skills: \n {unit.skills}  \n exp is: {exp}";
    }
    private void Awake()
    {
        _takeUnit.unitTake.AddListener(UpdateUnitInformation);
    }
}
