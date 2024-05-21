using Unity.Entities;

namespace DefaultNamespace
{
    public class EntityComponentListener
    {
        public EntityComponentListener()
        {
            EntityComponentManager.Instance.ComponentAdded += OnComponentAdded;
        }

        public void AddListener()
        {
            // how to do this....
        }
        
        private void OnComponentAdded(Entity entity, IComponentData component)
        {
            
            
        }
    }
}