using UnityEngine;
using UnityEngine.AI;
using UnitSpace.Skills;
using UnitSpace.Enums;
using UnitSpace.Attributes;

namespace UnitSpace
{
    public class Unit : MonoBehaviour
    {
        public UnitAttributes unitAttributes;
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
            unitAttributes = new UnitAttributes(this);
            skills = new UnitSkills(this);
            resourcePosition = GetComponentInChildren<ResourceUnitPosition>();
            unitSelector = GetComponentInChildren<UnitSelector>();
        }
        protected void CreateStandartAttributes()
        {
            unitAttributes.GetOrCreateAttribute<Strenght>();
            unitAttributes.GetOrCreateAttribute<Defence>();
            unitAttributes.GetOrCreateAttribute<Sensitivity>();
            unitAttributes.GetOrCreateAttribute<Speed>();
            unitAttributes.GetOrCreateAttribute<Strenght>();
            unitAttributes.GetOrCreateAttribute<Health>();
            if (TryGetComponent<LoadParamsToUnit>(out var load))
                load.LoadParams();
            //adding randomness
            unitAttributes.attributes[UnityEngine.Random.Range(0, 3)].LevelUp(UnityEngine.Random.Range(0, 3));
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
            return $"attributes: \n {unitAttributes} \n skills: \n {skills}";
        }
    }
}
