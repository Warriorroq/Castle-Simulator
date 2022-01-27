using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerCamera;
using UnitSpace;
using UnitSpace.Enums;
using System;
using UIScripts.UIUnitInfo;
namespace UIScripts.UIUnitsBtns
{
    public class BtnCreater : MonoBehaviour
    {
        [SerializeField] private Button _unitBtnPrefab;
        private Dictionary<UnitType, List<Button>> _unitButtons;
        private RectTransform _rectView;
        private UnitInfoReader _unitReader;
        private void Awake()
        {
            _unitReader = FindObjectOfType<UnitInfoReader>();
            Camera.main.GetComponent<GiveOrderToUnits>().takeUnits.AddListener(UpdateBtns);
            _unitButtons = new Dictionary<UnitType, List<Button>>();
            _rectView = GetComponent<RectTransform>();
            foreach (UnitType type in Enum.GetValues(typeof(UnitType)))
                _unitButtons.Add(type, new List<Button>());
        }
        private void UpdateBtns(List<Unit> arg0, UnitType arg1)
        {
            foreach(var btn in _unitButtons[arg1])
            {
                btn.onClick.RemoveAllListeners();
                Destroy(btn.gameObject);
            }
            _unitButtons[arg1].Clear();
            if (arg1 == UnitType.Core)
            {
                int i = 0;
                foreach (Unit unit in arg0)
                {
                    var btn = Instantiate(_unitBtnPrefab, Vector3.zero, Quaternion.identity, transform);                    
                    btn.transform.localPosition = new Vector3(100, -20 + i * -40, 0);
                    _unitButtons[arg1].Add(btn);
                    btn.onClick.AddListener(() => _unitReader.TakeUnit(unit));
                    i++;
                }
                _rectView.sizeDelta = new Vector2(0, 20 + i * 40);
                //_rectView.sizeDelta.Set(0, 20 + i * 40);
            }
        }
    }
}