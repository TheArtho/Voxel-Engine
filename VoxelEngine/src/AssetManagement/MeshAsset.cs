using VoxelEngine.Rendering.Models;

namespace VoxelEngine.AssetManagement
{
    public class MeshAsset : IAsset
    {
        public string Name { get; }
        public Mesh Mesh { get; }

        public MeshAsset(string name, Mesh mesh)
        {
            Name = name;
            Mesh = mesh;
        }
    }
}