namespace UnitSpace
{
    public class MineUnit : Unit
    {
        private ResourceMineContainer _container;
        public void Awake()
        {
            _container = GetComponent<ResourceMineContainer>();
        }
        public void Start(){}
        public void Update(){}
        public override string ToString()
        {
            return $"{_container}";
        }
    }
}
