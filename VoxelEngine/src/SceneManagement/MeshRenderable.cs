using VoxelEngine.Rendering;
using VoxelEngine.Rendering.Models;
using VoxelEngine.Rendering.Shaders;

namespace VoxelEngine.SceneManagement;

public class MeshRenderable(Mesh mesh, Shader shader) : IRenderable
{
    public void Render(Renderer renderer)
    {
        renderer.DrawMesh(mesh, shader);
    }
}