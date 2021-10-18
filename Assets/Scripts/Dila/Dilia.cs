using UnityEngine;
using UnityEngine.Events;

namespace Dila
{
    public class Dilia : MonoBehaviour
    {
        public UnityEvent startSaturn;
        public UnityEvent<int> startMoon;
        public UnityEvent endGame;
        [SerializeField] private int _daysLeft;
        [SerializeField] private float _timeForLap;
        [SerializeField] private float _saturnTime;
        void Start()
        {
            _saturnTime = _timeForLap * 3;
            Invoke(nameof(OnSaturnTime), _saturnTime);
        }
        private void OnSaturnTime()
        {
            startSaturn.Invoke();
            Invoke(nameof(OnMoonTime), _timeForLap);
            if (_daysLeft <= 0)
                endGame.Invoke();
        }
        private void OnMoonTime()
        {
            startMoon.Invoke(_daysLeft);
            Invoke(nameof(OnSaturnTime), _saturnTime);
            _daysLeft--;
        }
    }
}
