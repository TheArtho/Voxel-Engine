using VoxelEngine.Rendering;

namespace VoxelEngine.SceneManagement
{
    public class Scene
    {
        private readonly List<IRenderable> _renderables = [];

        public void AddRenderable(IRenderable renderable)
        {
            _renderables.Add(renderable);
        }

        public void RemoveRenderable(IRenderable renderable)
        {
            _renderables.Remove(renderable);
        }

        public void Render(Renderer renderer)
        {
            foreach (var renderable in _renderables)
            {
                renderable.Render(renderer);
            }
        }
    }
}