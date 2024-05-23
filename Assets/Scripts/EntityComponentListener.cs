using System;
using System.Collections.Generic;
using Components;
using Unity.Entities;
using UnityEngine;

namespace DefaultNamespace
{
    public class EntityComponentListener
    {
        private Dictionary<Type, Action<Entity, IComponentData>> _componentAddedListeners = new();
        
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
                    kvp.Value(entity, componentData);
                }
            }
        }
        
        public void AddComponentAddedListener<T>(Action<Entity, IComponentData> callback)
        {
            _componentAddedListeners.Add(typeof(T), callback);
        }
        
        private void OnPositionAdded(Entity arg1, IComponentData arg2)
        {
            var position = (Position)arg2;
            Debug.Log("position added " + position.Y);
        }
        
        private void TestAPI()
        {
            AddComponentAddedListener<Position>(OnPositionAdded);
        }

    }
}