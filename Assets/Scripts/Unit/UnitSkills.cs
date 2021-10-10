using System.Collections.Generic;
using System;
using UnitSpace.Interfaces;
namespace UnitSpace
{
    public class UnitSkills : IDisposable
    {
        public List<ISkillable> skills;
        private Unit _owner;
        public UnitSkills(Unit owner)
        {
            _owner = owner;
            skills = new List<ISkillable>();
        }
        public void AddSkill(ISkillable skill)
        {
            skill.SetUnitOwner(_owner);
            skills.Add(skill);
        }
        public void Dispose()
        {
            foreach (var item in skills)
                item.Dispose();
        }
        public void ActivateSkill<T>() where T : ISkillable
        {
            foreach (var item in skills)
                if (item.GetType() == typeof(T))
                    item.Use();
        }
        public void UpdateSkills(float time)
        {
            foreach (var item in skills)
                item.Update(time);
        }
        public override string ToString()
        {
            var text = string.Empty;
            foreach (var skill in skills)
                text += skill.ToString() + "\n";
            return text;
        }
    }
}
