
namespace UnitSpace.Orders
{
    public class DropResource : Order    
    {
        public override void EndOrder()
        {
            base.EndOrder();
            _owner.resourcePosition.DropResource();
        }
        protected override void OnUpdateOrder()
        {
            EndOrder();
        }
    }
}
