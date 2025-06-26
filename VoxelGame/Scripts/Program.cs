namespace VoxelGame
{
    public sealed class Program
    {
        private static void Main(string[] args)
        {
            var bootstraper = new Bootstraper();

            bootstraper.Initialize();
        }
    }
}
