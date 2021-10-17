using System.Collections;
using System.Collections.Generic;
using UnitSpace;
using UnityEngine;
using UnityEngine.UI;
public class CreateSkillButtons : MonoBehaviour
{
    [SerializeField] private List<Button> _skills;
    [SerializeField] private Button _prefab;
    public void Create(Unit unit)
    {
        Clear();
        var i = 0;
        foreach(var skill in unit.skills.skills)
        {
            var prefab = Instantiate(_prefab,transform);
            i++;
            var lastPos = prefab.transform.position;
            lastPos.y -= 40 * i;
            prefab.transform.position = lastPos;
            _skills.Add(prefab);
            prefab.onClick.AddListener(skill.Use);
        }
    }
    public void Clear()
    {
        foreach (var button in _skills)
            Destroy(button.gameObject);
        _skills.Clear();
    }
}
