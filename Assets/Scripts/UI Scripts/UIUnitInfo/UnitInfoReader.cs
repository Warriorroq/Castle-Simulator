using System.Collections;
using System.Collections.Generic;
using UnitSpace;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.UIUnitInfo
{
    public class UnitInfoReader : MonoBehaviour
    {
        [SerializeField] private Text _unitName;
        [SerializeField] private Text _unitInfo;
        [SerializeField] private Unit _takedUnit;
        //[SerializeField] private CreateSkillButtons _createSkillButtons;
        public void ClearInformation()
        {
            _unitName.text = string.Empty;
            _unitInfo.text = string.Empty;
            //_createSkillButtons.Clear();
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
                //_createSkillButtons.Create(_takedUnit);
                _unitName.text = _takedUnit.name;
                _unitInfo.text = _takedUnit.ToString();
            }
            else
                ClearInformation();
        }
        private void Awake()
        {
            InvokeRepeating(nameof(UpdateInformationAboutUnit), 0, .05f);
        }
    }
}
