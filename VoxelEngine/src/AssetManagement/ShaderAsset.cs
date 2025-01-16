using VoxelEngine.AssetManagement;
using VoxelEngine.Rendering.Shaders;

namespace VoxelEngine.AssetManagement
{
    public class ShaderAsset : IAsset
    {
        public string Name { get; }
        public Shader Shader { get; }

        public ShaderAsset(string name, Shader shader)
        {
            Name = name;
            Shader = shader;
        }
    }
}