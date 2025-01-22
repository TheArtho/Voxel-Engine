using VoxelEngine.AssetManagement;
using VoxelEngine.Core.Objects;
using VoxelEngine.Core.Objects.Modules;
using VoxelEngine.Rendering;

namespace VoxelEngine.SceneManagement
{
    public class Scene
    {
        public static Scene? Instance { get; private set; }
        
        private readonly List<GameObject> _gameObjects = [];
        private readonly List<IRenderable> _renderables = [];

        public Scene()
        {
            Instance ??= this;
            Setup();
        }

        private void Setup()
        {
            var testObject = new GameObject("Test Object");

            testObject.AddModule<MeshRenderer>();

            testObject.GetModule<MeshRenderer>().mesh = AssetManager.Instance.GetAsset<MeshAsset>("test").Mesh;
            testObject.GetModule<MeshRenderer>().shader = AssetManager.Instance.GetAsset<ShaderAsset>("simple").Shader;
            
            GameObject.Instantiate(testObject);
        }

        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
            
            Console.WriteLine($"Added game object {gameObject.Name}.");
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        public void AddRenderable(IRenderable renderable)
        {
            _renderables.Add(renderable);
            
            Console.WriteLine($"Added renderable.");
        }

        public void RemoveRenderable(IRenderable renderable)
        {
            _renderables.Remove(renderable);
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.UpdateModules(deltaTime);
            }
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