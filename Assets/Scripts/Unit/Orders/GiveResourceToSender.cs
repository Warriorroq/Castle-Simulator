using UnityEngine;

namespace UnitSpace.Orders
{
    public class GiveResourceToSender : Order
    {
        private ResourceSender _sender;
        public GiveResourceToSender(ResourceSender sender)
        {
            _sender = sender;
        }
        public override void StartOrder()
        {
            base.StartOrder();
            _owner.resourcePosition.DropResource(out var resource);
            _sender.SendResource(resource);
            EndOrder();
        }
    }
}
