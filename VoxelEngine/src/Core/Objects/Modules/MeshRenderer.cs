using Assimp;
using VoxelEngine.Core.Windowing;
using VoxelEngine.Rendering;
using VoxelEngine.Rendering.Shaders;
using VoxelEngine.SceneManagement;
using Mesh = VoxelEngine.Rendering.Models.Mesh;
using Scene = VoxelEngine.SceneManagement.Scene;

namespace VoxelEngine.Core.Objects.Modules;

public class MeshRenderer : Module, IRenderable
{
    public Mesh? mesh;
    public Shader? shader;

    public override void OnInit()
    {
        Scene.Instance?.AddRenderable(this);
    }

    public override void OnDestroy()
    {
        Scene.Instance?.RemoveRenderable(this);
    }

    public void Render(Renderer renderer)
    {
        // TODO Change architecture to handle several cameras
        if (Camera.MainCamera == null)  
        {
            return;
        }

        if (mesh == null || shader == null)
        {
            return;
        }
        
        Console.WriteLine($"Rendering mesh of {GameObject.Name}");
        
        renderer.DrawMesh(mesh, shader, GameObject.Transform.GetTransformationMatrix(), Camera.MainCamera.GetViewMatrix(), Camera.MainCamera.GetProjectionMatrix());
    }
}