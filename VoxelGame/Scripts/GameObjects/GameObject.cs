using VoxelGame.GameObjects.Components;
using VoxelGame.Graphics;
using VoxelGame.Worlds;

namespace VoxelGame.GameObjects
{
    public abstract class GameObject
    {
        public readonly Transform Transform = new Transform();
        public readonly World World;

        public readonly bool IsStatic;

        public GameObject(World world, bool isStatic = false)
        {
            World = world;
            IsStatic = isStatic;
        }

        public virtual void Load() { }
        public virtual void Unload() { }
        public virtual void Render() { }
        public virtual void Tick() { }
        public virtual void Update(float deltaTime) { }
        public virtual bool CanRender(Frustum frustum) => true;
    }
}
