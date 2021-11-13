using UnityEngine;
using Resource;
using System.Collections.Generic;
using System;

namespace UnitSpace
{
    public class ResourceSender : MonoBehaviour
    {
        private PlayerResources _playerResources;
        private void Awake()
        {
            _playerResources = Camera.main.GetComponent<PlayerResources>();
        }
        public void SendResource(ResourceObject resourceObject)
        {
            _playerResources.TakeResources(resourceObject.resourceType, resourceObject.Amount);
            Destroy(resourceObject.gameObject);
        }
    }
}
