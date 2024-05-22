using System;
using System.Collections.Generic;
using Components;
using Unity.Entities;

namespace DefaultNamespace
{
    public class EntityComponentListener
    {
        private Dictionary<Type, Delegate> _listeners = new();
        public EntityComponentListener()
        {
            EntityComponentManager.Instance.ComponentAdded += OnComponentAdded;
        }

        public void AddListener<T>(Func<T> callback)
        {
            _listeners.Add(typeof(T), callback);
        }
        
        private void OnComponentAdded(Entity entity, IComponentData componentData)
        {
            foreach (var kvp in _listeners)
            {
                if (kvp.Key == componentData.GetType())
                {
                    kvp.Value.DynamicInvoke(entity, componentData);
                }
            }
        }

        private void TestAPI()
        {
            AddListener<Position>(OnPositionChanged);

            Position OnPositionChanged<T>(Entity entity, T component)
            {
                
            }
        }
    }
}