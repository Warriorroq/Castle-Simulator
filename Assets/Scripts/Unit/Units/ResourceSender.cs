using UnityEngine;
using Resource;
using System.Collections.Generic;

public class ResourceSender : MonoBehaviour
{
    private PlayerResources _playerResources;
    private void Awake()
    {
        _playerResources = Camera.main.GetComponent<PlayerResources>();
    }
    public void Send(ResourceObject resourceObject)
    {
        _playerResources.TakeResources(resourceObject.resourceType, resourceObject.Amount);
        Destroy(resourceObject);
    }
}
