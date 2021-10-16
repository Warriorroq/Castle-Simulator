using UnitSpace.Interfaces;
using UnitSpace.Attributes;
namespace UnitSpace.Skills
{
    public class Dash : ISkillable
    {
        private ISkillable.skillState _state;
        private float _startSpeed;
        private float _reloadTimer;
        private Speed _speed;
        private float _speedAfterChanges;
        private Unit _owner;
        public void SetUnitOwner(Unit owner)
        {
            _owner = owner;
            _speed = owner.attributes.GetOrCreateAttribute<Speed>();
            _state = ISkillable.skillState.ready;
        }
        public void IteractWith(Unit unit){}
        public void Use()
        {
            if(_state == ISkillable.skillState.ready)
            {
                _state = ISkillable.skillState.reloading;
                _reloadTimer = 0;
                _startSpeed = _speed.value;
                _speed.value = _startSpeed * 2;
                _speedAfterChanges = _speed.value;
                _owner.navMeshAgent.speed = _speed.value;
            }
        }
        public void Update(float time)
        {
            if (_state == ISkillable.skillState.reloading)
            {
                _reloadTimer += time;
                if (_reloadTimer > 3)
                {
                    _speed.value = _speed.value - _speedAfterChanges + _startSpeed;
                    _speedAfterChanges = 0;
                    _startSpeed = 0;
                    _owner.navMeshAgent.speed = _speed.value;
                }
                if (_reloadTimer >= 10)
                    _state = ISkillable.skillState.ready;                
            }
        }
        public void Dispose(){}
        public override string ToString()
        {
            return $"Dash - increases speed by 100%";
        }
        public void UseBySkill(IOrder skill){}
    }
}
