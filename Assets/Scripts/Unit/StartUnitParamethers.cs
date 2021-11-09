using System.Collections.Generic;
using UnityEngine;

namespace UnitSpace
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StartUnitParams", order = 1)]
    public class StartUnitParamethers : ScriptableObject
    {
        [SerializeField] private List<AttributesParam> startParams;
        public int Count => startParams.Count;
        public AttributesParam this[int index]
            => startParams[index];
    }
    [System.Serializable]
    public struct AttributesParam
    {
        public string name;
        public float value;
        public float maxValue;
    }
}
