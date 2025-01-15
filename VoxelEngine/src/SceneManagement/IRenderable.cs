using VoxelEngine.Rendering;

namespace VoxelEngine.SceneManagement
{
    public interface IRenderable
    {
        void Render(Renderer? renderer);
    }
}