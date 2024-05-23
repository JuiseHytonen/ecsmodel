using System;
using System.Collections.Generic;
using Components;
using Unity.Entities;
using UnityEngine;

namespace DefaultNamespace
{
    public class EntityComponentListener
    {
        private Dictionary<Type, Delegate> _componentAddedListeners = new();
        
        public EntityComponentListener()
        {
            EntityComponentManager.Instance.ComponentAdded += OnComponentAdded;
            TestAPI();
        }
        
        private void OnComponentAdded(Entity entity, IComponentData componentData)
        {
            foreach (var kvp in _componentAddedListeners)
            {
                if (kvp.Key == componentData.GetType())
                {
                    kvp.Value.DynamicInvoke(entity, componentData);
                }
            }
        }
        
        public void AddComponentAddedListener<T>(Action<Entity, T> callback)
        {
            _componentAddedListeners.Add(typeof(T), callback);
        }
        
        public void OnPositionChanged<T>(Entity entity, T component)
        {
            Debug.Log("position component added");
        }
        
        private void TestAPI()
        {
            AddComponentAddedListener<Position>(OnPositionChanged);
        }
    }
}