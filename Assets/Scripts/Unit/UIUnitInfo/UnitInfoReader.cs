using System.Collections;
using System.Collections.Generic;
using UnitSpace;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoReader : MonoBehaviour
{
    [SerializeField] private Text _unitName;
    [SerializeField] private Text _unitInfo;
    [SerializeField] private Unit _takedUnit;
    [SerializeField] private CreateSkillButtons _createSkillButtons;
    public void ClearInformation()
    {
        _unitName.text = string.Empty;
        _unitInfo.text = string.Empty;
        _createSkillButtons.Clear();
    }
    public void TakeUnit(Unit unit)
    {
        _takedUnit = unit;
        UpdateInformationAboutUnit();
    }
    private void UpdateInformationAboutUnit()
    {
        if (_takedUnit)
        {
            _createSkillButtons.Create(_takedUnit);
            LevelingUpByAttributes.GetInstance().CountUnitTotalExp(_takedUnit, out var exp);
            _unitName.text = _takedUnit.name;
            _unitInfo.text = $"attributes: \n {_takedUnit.attributes} \n skills: \n {_takedUnit.skills}  \n exp is: {exp}";
        }
        else
            ClearInformation();
    }
    private void Awake()
    {
        FindObjectOfType<OrderGiver>().unitTake.AddListener(TakeUnit);
        InvokeRepeating(nameof(UpdateInformationAboutUnit), 0, .1f);
    }
}
