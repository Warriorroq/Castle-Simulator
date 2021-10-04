using System;
namespace UnitSpace.Interfaces
{
    public interface ISkillable : ISetUnitOwner, IUsable, IDisposable
    {
        public abstract void IteractWith(Unit unit);
    }
}
