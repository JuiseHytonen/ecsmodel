using System;
using System.Collections.Generic;
using Components;
using Unity.Entities;
using UnityEngine;

namespace DefaultNamespace
{
    public class EntityComponentListener
    {
        static private Dictionary<Type, ComponentListener<IComponentData>> _componentAddedListeners = new();
        
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
        
        public void AddComponentAddedListener<T>(IComponentListener<IComponentData> componentListener) where T : IComponentData
        {
            _componentAddedListeners.Add(typeof(T), componentListener);
        }
        
        private void TestAPI()
        {
            var componentListener = new ComponentListener<Position>();
            AddComponentAddedListener<Position>(componentListener);
            componentListener.TypedComponentAdded += OnPositionAdded;
        }
        
        private void OnPositionAdded(Entity arg1, Position arg2)
        {
            var position = (Position)arg2;
            Debug.Log("position added " + position.Y);
        }

        public interface IComponentListener<in T> where T : IComponentData
        {
            event Action<Entity, T> TypedComponentAdded;

            public void InvokeEvent(Entity entity, T component);
        }
        
        public class ComponentListener<T> : IComponentListener<T> where T : IComponentData
        {
            public event Action<Entity, T> TypedComponentAdded;
            public void InvokeEvent(Entity entity, T component)
            {
                TypedComponentAdded?.Invoke(entity, component);
            }
        }
    }
}