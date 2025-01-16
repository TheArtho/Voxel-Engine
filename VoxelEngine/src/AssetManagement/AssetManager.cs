namespace VoxelEngine.AssetManagement
{
    public class AssetManager
    {
        private readonly Dictionary<Type, Dictionary<string, IAsset>> _assets = new();

        public void RegisterAsset<T>(string name, T asset) where T : IAsset
        {
            var type = typeof(T);
            if (!_assets.TryGetValue(type, out Dictionary<string, IAsset>? value))
            {
                value = new Dictionary<string, IAsset>();
                _assets[type] = value;
            }

            value[name] = asset;
        }

        public T GetAsset<T>(string name) where T : IAsset
        {
            var type = typeof(T);

            if (_assets.TryGetValue(type, out var assets) && assets.TryGetValue(name, out var asset))
            {
                return (T)asset;
            }

            throw new Exception($"Asset of type {type.Name} with name '{name}' not found.");
        }

        public bool HasAsset<T>(string name) where T : IAsset
        {
            var type = typeof(T);
            return _assets.TryGetValue(type, out var assets) && assets.ContainsKey(name);
        }
    }
}