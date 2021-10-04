using UnityEngine;
using UnitSpace.Interfaces;
using UnitSpace.Attributes;
using System.Collections;

namespace UnitSpace.Skills
{
    public class Dash : ISkillable
    {
        private ISkillable.skillState _state;
        private float _startSpeed;
        private float _reloadTimer;
        private Speed _speed;
        private float _speedAfterChanges;
        public void SetUnitOwner(Unit owner)
        {
            _speed = owner.attributes.GetAttribute<Speed>();
            _state = ISkillable.skillState.ready;
        }
        public void IteractWith(Unit unit){}
        public void Use()
        {
            if(_state is ISkillable.skillState.ready)
            {
                _state = ISkillable.skillState.reloading;
                _reloadTimer = 0;
                _startSpeed = _speed.value;
                _speed.value = _startSpeed * 2;
                _speedAfterChanges = _speed.value;
                Debug.Log("Dash is used");
            }
        }
        public void Update(float time)
        {
            if (_state is ISkillable.skillState.reloading)
                _reloadTimer += time;
            if (_reloadTimer > 3)
            {
                _speed.value = _speed.value - _speedAfterChanges + _startSpeed;
                _speedAfterChanges = 0;
                _startSpeed = 0;
            }
            if(_reloadTimer >= 10)
                _state = ISkillable.skillState.ready;
        }
        public void Dispose(){}
    }
}
