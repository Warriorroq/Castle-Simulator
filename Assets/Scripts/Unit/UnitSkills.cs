using System.Collections.Generic;
using System;
using UnitSpace.Interfaces;
namespace UnitSpace
{
    public class UnitSkills : IDisposable
    {
        private List<ISkillable> _skills;
        private Unit _owner;
        public UnitSkills(Unit owner)
        {
            _owner = owner;
            _skills = new List<ISkillable>();
        }
        public void AddSkill(ISkillable skill)
        {
            skill.SetUnitOwner(_owner);
            _skills.Add(skill);
        }
        public void Dispose()
        {
            foreach (var item in _skills)
                item.Dispose();
        }
    }
}
