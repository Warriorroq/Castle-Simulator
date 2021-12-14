using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsUI : MonoBehaviour
{
    [SerializeField] private RectTransform _commands;
    void Start()
    {
        _commands.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StopAllCoroutines();
            _commands.position = Input.mousePosition;
            _commands.gameObject.SetActive(true);
        }
    }
}
