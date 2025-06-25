namespace VoxelGame.Worlds
{
    public sealed class TickSystem
    {
        public const int TPS = 20;
        public const float Delta = 1f / TPS;

        private readonly World _world;

        private float _timer;

        public TickSystem(World world)
        {
            _world = world;
        }

        public void Calculate(float deltaTime)
        {
            _timer += deltaTime;
        }

        public void Tick()
        {
            if (_timer < Delta)
            {
                return;
            }

            _timer -= Delta;

            _world.Tick();
        }
    }
}
