using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnitSpace.Skills;
using UnitSpace.Attributes;

namespace UnitSpace
{
    public class Unit : MonoBehaviour
    {
        public UnitAttributes attributes;
        public UnitSkills skills;
        public UnitOrderUser unitOrders;
        public NavMeshAgent navMeshAgent;
        public HealthComponent healthComponent;
        private void Awake()
        {
            attributes = new UnitAttributes(this);
            skills = new UnitSkills(this);
            TryGetComponent(out unitOrders);
            TryGetComponent(out healthComponent);
            TryGetComponent(out navMeshAgent);
            skills.AddSkill(new Dash());
        }
        private void Update()
        {
            skills.UpdateSkills(Time.deltaTime);
        }
    }
}
