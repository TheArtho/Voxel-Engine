using VoxelEngine.Rendering.Textures;

namespace VoxelEngine.AssetManagement
{
    public class TextureAsset : IAsset
    {
        public string Name { get; }
        public Texture Texture { get; }

        public TextureAsset(string name, Texture texture)
        {
            Name = name;
            Texture = texture;
        }
    }
}