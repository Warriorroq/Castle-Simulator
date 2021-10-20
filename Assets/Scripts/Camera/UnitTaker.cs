using System.Collections.Generic;
using UnitSpace;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerCamera
{
    public class UnitTaker : MonoBehaviour
    {
        public UnityEvent<List<Unit>> takeUnits;
        [SerializeField]private string _unitTag;
        private Rect _rect;
        private List<Unit> _takedUnits;
        private Vector2 _onClick;
        private Vector2 _endClick;
        [SerializeField]private List<RectTransform> _canvasRects;
        private void Awake()
        {
            _takedUnits = new List<Unit>();
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _onClick = Input.mousePosition;
            if (Input.GetMouseButtonUp(0))
            {
                _endClick = Input.mousePosition;
                if(!TochesCanvasRects(_endClick))
                {
                    CreateRect();
                    GetAllObjectsFromRect();
                }
            }
        }
        private bool TochesCanvasRects(Vector2 position)
        {
            foreach(var rect in _canvasRects)
            {
                if (rect.gameObject.activeSelf)
                    if (rect.rect.Contains(position))
                        return true;
            }
            return false;
        }
        private void CreateRect()
        {
            SetRectPos();
            SetRectSize();
        }
        private void GetAllObjectsFromRect()
        {
            if(!Input.GetKey(KeyCode.LeftShift))
                _takedUnits.Clear();
            var units = GameObject.FindGameObjectsWithTag(_unitTag);
            foreach(var unit in units)
            {
                var unitPositionOnScreen = Camera.main.WorldToViewportPoint(unit.transform.position);
                unitPositionOnScreen.y *= Screen.height;
                unitPositionOnScreen.x *= Screen.width;
                if (_rect.Contains(unitPositionOnScreen))
                    _takedUnits.Add(unit.GetComponent<Unit>());
            }
            takeUnits.Invoke(_takedUnits);
        }
        private void SetRectSize()
        {
            _rect.width = Mathf.Abs(_onClick.x - _endClick.x);
            _rect.height = Mathf.Abs(_onClick.y - _endClick.y);
        }
        private void SetRectPos()
        {
            Vector2 rectStartPos = new Vector2();
            rectStartPos.x = _onClick.x > _endClick.x ? _endClick.x : _onClick.x;
            rectStartPos.y = _onClick.y > _endClick.y ? _endClick.y : _onClick.y;
            _rect.position = rectStartPos;
        }
    }
}
