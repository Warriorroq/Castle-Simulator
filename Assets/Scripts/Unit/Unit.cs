using UnityEngine;
using UnityEngine.AI;
using UnitSpace.Skills;
using UnitSpace.Enums;
namespace UnitSpace
{
    public class Unit : MonoBehaviour
    {
        public UnitAttributes attributes;
        public UnitSkills skills;
        public UnitOrderUser unitOrders;
        public NavMeshAgent navMeshAgent;
        public HealthComponent healthComponent;
        public UnitSelector unitSelector;
        public UnitFraction fraction;
        private void Awake()
        {
            attributes = new UnitAttributes(this);
            skills = new UnitSkills(this);
            TryGetComponent(out unitOrders);
            TryGetComponent(out healthComponent);
            TryGetComponent(out navMeshAgent);
            unitSelector = GetComponentInChildren<UnitSelector>();
        }
        private void Start()
        {
            var rand = Random.Range(1, 9);
            for(int i = 0; i< rand;i++)
            {
                skills.AddSkill(new Dash());
            }
            skills.AddSkill(new CreateTimeWall());
        }
        private void Update()
        {
            skills.UpdateSkills(Time.deltaTime);
        }
    }
}
