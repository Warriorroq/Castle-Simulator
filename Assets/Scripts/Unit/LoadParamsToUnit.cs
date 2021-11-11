using UnityEngine;
using UnitSpace;
public class LoadParamsToUnit : MonoBehaviour
{
    [SerializeField] private Unit _owner;
    [SerializeField] private StartUnitParamethers _load;
    private void Awake()
    {
        _owner = GetComponent<Unit>();
    }
    public void LoadParams()
    {
        if (_load)
        {
            for(int i = 0; i< _load.Count; i++)
                _owner.attributes[_load[i].name]?
                      .LoadAttribute(_load[i]);
        }
    }
}
