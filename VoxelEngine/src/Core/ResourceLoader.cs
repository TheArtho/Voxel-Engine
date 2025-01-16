using Silk.NET.OpenGL;
using VoxelEngine.AssetManagement;
using VoxelEngine.Rendering.Models;
using Shader = VoxelEngine.Rendering.Shaders.Shader;
using Texture = VoxelEngine.Rendering.Textures.Texture;

namespace VoxelEngine.Core
{
    public class ResourceLoader(GL gl, AssetManager assetManager)
    {
        private void LoadShaders(string directoryPath)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath, "*_vertex.glsl"))
            {
                var name = Path.GetFileNameWithoutExtension(filePath);
                var prefix = name.Replace("_vertex", "");
                
                var vertexPath = Path.Combine(directoryPath, $"{prefix}_vertex.glsl");
                var fragmentPath = Path.Combine(directoryPath, $"{prefix}_fragment.glsl");

                if (File.Exists(vertexPath) && File.Exists(fragmentPath))
                {
                    var shader = new Shader(gl, vertexPath, fragmentPath);
                    assetManager.RegisterAsset(name, new ShaderAsset(name, shader));
                    Console.WriteLine($"Loaded Shader: {prefix}_vertex and {prefix}_fragment");
                }
                else
                {
                    Console.WriteLine($"Failed to load shader: {filePath}: missing fragment shader");
                }
            }
        }

        private void LoadTextures(string directoryPath)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath, "*.png"))
            {
                var name = Path.GetFileNameWithoutExtension(filePath);
                var texture = new Texture(gl, filePath);
                assetManager.RegisterAsset(name, new TextureAsset(name, texture));
                Console.WriteLine($"Loaded Texture: {name}");
            }
        }

        private void LoadMeshes(string directoryPath)
        {
            foreach (var filePath in Directory.GetFiles(directoryPath, "*.obj"))
            {
                var name = Path.GetFileNameWithoutExtension(filePath);
                var mesh = Mesh.LoadFromFile(gl, filePath);
                assetManager.RegisterAsset(name, new MeshAsset(name, mesh));
                Console.WriteLine($"Loaded Mesh: {name}");
            }
        }

        public void LoadAll(string rootDirectory)
        {
            LoadShaders(Path.Combine(rootDirectory, "Shaders"));
            LoadTextures(Path.Combine(rootDirectory, "Textures"));
            LoadMeshes(Path.Combine(rootDirectory, "Models"));
        }
    }
}
