using VoxelEngine.Core.Objects.Modules;
using VoxelEngine.SceneManagement;

namespace VoxelEngine.Core.Objects
{
    public class GameObject : Object
    {
        private readonly List<Module> _modules = [];

        public string Name { get; set; }
        public readonly Transform Transform;

        public static void Instantiate(GameObject gameObject, Transform? parent = null)
        {
            if (parent != null)
            {
                gameObject.Transform.Parent = parent;
            }
            
            Scene.Instance.AddGameObject(gameObject);
        }

        public GameObject(string name = "GameObject")
        {
            Name = name;
            // Each GameObject has one Transform by default
            this.Transform = new Transform
            {
                GameObject = this
            };
            this.Transform.OnInit();
        }

        /// <summary>
        /// Add a Module to the GameObject
        /// </summary>
        /// <typeparam name="T">Module type</typeparam>
        /// <returns>Module added</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T AddModule<T>() where T : Module, new()
        {
            if (typeof(T) == typeof(Transform))
            {
                throw new InvalidOperationException($"Cannot add another Transform Module to {Name}.");
            }
            
            if (HasModule<T>())
            {
                throw new InvalidOperationException($"Module of type {typeof(T).Name} already exists on {Name}.");
            }

            T module = new()
            {
                GameObject = this
            };
            _modules.Add(module);
            module.OnInit();
            return module;
        }

        /// <summary>
        /// Get a Module of the GameObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T GetModule<T>() where T : Module
        {
            return _modules.OfType<T>().FirstOrDefault() ?? throw new InvalidOperationException($"Module of type {typeof(T).Name} not found on {Name}.");
        }

        /// <summary>
        /// Verify is the GameObject has a Module
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HasModule<T>() where T : Module
        {
            return _modules.OfType<T>().Any();
        }

        /// <summary>
        /// Removes a Module from the GameObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveModule<T>() where T : Module
        {
            var module = GetModule<T>();
            _modules.Remove(module);
            module.OnDestroy();
        }

        /// <summary>
        /// Update all modules
        /// </summary>
        /// <param name="deltaTime"></param>
        public void UpdateModules(float deltaTime)
        {
            foreach (var module in _modules)
            {
                module.OnUpdate(deltaTime);
            }
        }
    }
}