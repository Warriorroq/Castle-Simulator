using UnityEngine;
using UnityEngine.Events;
using UnitSpace.Interfaces;
namespace UnitSpace
{
    public class Unit : MonoBehaviour
    {
        public UnitAttributes attributes;
        public UnitSkills skills;
        public UnitOrderUser unitOrders;
        public UnityEvent<IteractData> giveIteractData;
        public UnityEvent<IteractData> takeIteractData;
        private void Awake()
        {
            attributes = new UnitAttributes(this);
            skills = new UnitSkills(this);
            TryGetComponent(out unitOrders);
        }
    }
}
