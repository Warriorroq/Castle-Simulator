using UnityEngine;
using UnityEngine.AI;
using UnitSpace.Skills;
using UnitSpace.Enums;
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
        public UnitSelector unitSelector;
        public ResourceUnitPosition resourcePosition;
        public UnitType fraction;
        private void Awake()
        {
            GetUnitComponents();
        }
        protected void GetUnitComponents()
        {
            TryGetComponent(out unitOrders);
            TryGetComponent(out healthComponent);
            TryGetComponent(out navMeshAgent);
            attributes = new UnitAttributes(this);
            skills = new UnitSkills(this);
            resourcePosition = GetComponentInChildren<ResourceUnitPosition>();
            unitSelector = GetComponentInChildren<UnitSelector>();
        }
        protected void CreateStandartAttributes()
        {
            attributes.GetOrCreateAttribute<Strenght>();
            attributes.GetOrCreateAttribute<Defence>();
            attributes.GetOrCreateAttribute<Sensitivity>();
            attributes.GetOrCreateAttribute<Speed>();
            attributes.GetOrCreateAttribute<Strenght>();
            attributes.GetOrCreateAttribute<Health>();
            if (TryGetComponent<LoadParamsToUnit>(out var load))
                load.LoadParams();
        }
        private void Start()
        {
            CreateStandartAttributes();
        }
        private void Update()
        {
            skills.UpdateSkills(Time.deltaTime);
        }
        public override string ToString()
        {
            return $"attributes: \n {attributes} \n skills: \n {skills}";
        }
    }
}
