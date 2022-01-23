using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UIScripts.UITimeScript{

    [RequireComponent(typeof(Slider))]
    public class UITimeChanger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _TextValue;
        public void ChangeValue(float arg0)
        {
            _TextValue.text = $"{arg0:0.0}";
            Time.timeScale = arg0;
        }
        private void Awake()
        {
            if (_TextValue is null)
            {
                GetTextValueFromHandler();
            }
        }
        private void Start()
        {
            var slider = GetComponent<Slider>();
            _TextValue.text = $"{slider.value:0.0}";
            slider.onValueChanged.AddListener(ChangeValue);
        }
        private void GetTextValueFromHandler()
        {
            foreach (Transform obj in transform)
            {
                if (obj.name == "ValueText")
                {
                    if (obj.TryGetComponent<TextMeshProUGUI>(out var comp))
                    {
                        _TextValue = comp;
                    }
                    else
                    {
                        throw new System.Exception("No Text component On handler");
                    }
                }
            }
        }
    }
}
