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
        public ResourcePosition resourcePosition;
        public UnitFraction fraction;
        private void Awake()
        {
            attributes = new UnitAttributes(this);
            skills = new UnitSkills(this);
            TryGetComponent(out unitOrders);
            TryGetComponent(out healthComponent);
            TryGetComponent(out navMeshAgent);
            resourcePosition = GetComponentInChildren<ResourcePosition>();
            unitSelector = GetComponentInChildren<UnitSelector>();
        }
        private void CreateStandartAttributes()
        {
            attributes.GetOrCreateAttribute<Strenght>();
            attributes.GetOrCreateAttribute<Defence>();
            attributes.GetOrCreateAttribute<Sensitivity>();
            attributes.GetOrCreateAttribute<Speed>();
            attributes.GetOrCreateAttribute<Strenght>();
            attributes.GetOrCreateAttribute<Health>();
            TryReadParams();
        }
        private void TryReadParams()
        {
            if(TryGetComponent<LoadParamsToUnit>(out var loadParamsToUnit))
                loadParamsToUnit.LoadParams();
        }
        private void Start()
        {
            CreateStandartAttributes();
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
