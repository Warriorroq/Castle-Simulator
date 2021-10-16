using UnitSpace.Interfaces;
using UnityEngine;
namespace UnitSpace.Skills
{
    public class CreateTimeWall : ISkillable
    {
        private ISkillable.skillState _state;       
        private float _reloadTimer;
        private Unit _owner;
        public void SetUnitOwner(Unit owner)
        {
            _owner = owner;
            _state = ISkillable.skillState.ready;
        }
        public void IteractWith(Unit unit) { }
        public void Use()
        {
            if (_state == ISkillable.skillState.ready)
            {
                _state = ISkillable.skillState.reloading;
                _reloadTimer = 0;
                CreateWall();
            }
        }
        public void Update(float time)
        {
            if (_state == ISkillable.skillState.reloading)
            {
                _reloadTimer += time;
                if (_reloadTimer >= 10)
                    _state = ISkillable.skillState.ready;
            }
        }
        public void Dispose() { }
        public override string ToString()
        {
            return $"Wall - creates wall in front of unit";
        }
        public void UseBySkill(IOrder skill) { }
        private void CreateWall()
        {
            Vector3 fwd = _owner.transform.TransformDirection(Vector3.forward);
            if (!Physics.Raycast(_owner.transform.position, fwd, out var hit, 1))
            {
                var a = Object.Instantiate(Resources.Load("Prefabs/wall"), _owner.transform.position + fwd,_owner.transform.rotation);
                Debug.Log(a.name);
            }
        }
    }
}