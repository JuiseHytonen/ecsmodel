using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct Position : IComponentData
    {
        public float3 Value;

        public float X => Value.x;
        public float Y => Value.y;
        public float Z => Value.z;
    }
}