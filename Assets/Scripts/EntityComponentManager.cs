using System;
using Components;
using Unity.Entities;
using UnityEngine;

namespace DefaultNamespace
{
    public class EntityComponentManager : MonoBehaviour
    {
        public static EntityComponentManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private EntityManager EntityManager => World.DefaultGameObjectInjectionWorld.EntityManager;
        
        private void Update()
        {
            var entity = CreateEntity();
            AddComponent(entity);
        }

        public Entity CreateEntity()
        {
            var components = new ComponentType[] { typeof(Position) };
            return EntityManager.CreateEntity(components);
        }

        public void AddComponent(Entity entity)
        {
            EntityManager.AddComponent<Position>(entity);
        }
    }
}