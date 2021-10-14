using System;
namespace UnitSpace.Interfaces
{
    public interface ISkillable : ISetUnitOwner, IUsable, IDisposable
    {
        enum skillState { 
            reloading,
            ready
        }

        public void Update(float time);
        public abstract void IteractWith(Unit unit);
        public abstract void UseBySkill(IOrder skill);
    }
}
