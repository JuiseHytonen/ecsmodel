using System;
using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class EntityComponentManager : MonoBehaviour
    {
        public static EntityComponentManager Instance;
        public EntityComponentListener Listener;

        public event Action<Entity, IComponentData> ComponentAdded;

        private void Awake()
        {
            Instance = this;
            Listener = new EntityComponentListener();
        }

        // Exposed as public to enable making queries and modifying entities and components
        public EntityManager EntityManager => World.DefaultGameObjectInjectionWorld.EntityManager;
        
        private void Update()
        {
            // All these are just examples of how entities API:s could be called from outside
            var entity = CreateEmptyEntity();
            var positionComponent = new Position() { Value = new float3(2.3f, 4.3f, 3.3f) };
            AddComponent(entity, positionComponent);
            var query = EntityManager.CreateEntityQuery(typeof(Position));
            print($"Num of position components {query.ToEntityArray(Allocator.TempJob).Length}");
        }

        public Entity CreateEmptyEntity()
        {
            var components = new ComponentType[] { };
            return EntityManager.CreateEntity(components);
        }

        public void AddComponent<T>(Entity entity, T component) where T : unmanaged, IComponentData
        {
            EntityManager.AddComponentData(entity, component);
            ComponentAdded?.Invoke(entity, component);
        }
    }
}