using System.Collections.Generic;
using UnitSpace.Interfaces;
namespace UnitSpace
{   
    public class IteractData
    {
        public float damage;
        public float healing;
        public List<ISkillable> skills;
        public IteractData(float damage = 0, float healing = 0, List<ISkillable> skills = null)
        {
            this.damage = damage;
            this.healing = healing;
            this.skills = skills is null ? new List<ISkillable>() : skills;

        }
    }
}
