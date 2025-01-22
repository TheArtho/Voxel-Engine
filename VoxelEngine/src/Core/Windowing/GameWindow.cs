using System.Numerics;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using VoxelEngine.AssetManagement;
using VoxelEngine.Core.Input;
using VoxelEngine.Rendering;
using VoxelEngine.SceneManagement;
using Silk.NET.Maths;

namespace VoxelEngine.Core.Windowing;

public class GameWindow
{
    private readonly IWindow _window;
    private InputManager _inputManager = null!;
    private GL _gl = null!;
    private Renderer _renderer = null!;
    private Scene _currentScene = null!;
    private AssetManager _assetManager = null!;
    private Camera _camera = null!;

    private readonly Vector2D<int> _resolution = new Vector2D<int>(1280, 720);

    public GameWindow()
    {
        var options = WindowOptions.Default;
        options.Size = _resolution;
        options.Title = "Voxel GL";

        _window = Window.Create(options);
        _window.Load += OnLoad;
        _window.Render += OnRender;
        _window.Update += OnUpdate;
    }

    private void OnLoad()
    {
        // OpenGL setup
        _gl = GL.GetApi(_window);
        _gl.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        
        // Asset Manager
        _assetManager = new AssetManager();
        // Resource Loader
        var resourceLoader = new ResourceLoader(_gl, _assetManager);
        resourceLoader.LoadAll("../../../src/Resources");
        
        // Renderer setup
        _renderer = new Renderer(_gl);
        // Scene setup
        _currentScene = new Scene();

        // Input Manager setup
        _inputManager = new InputManager();
        _inputManager.Initialize(_window);

        _camera = new Camera(new Vector3(0, 0, -10), Vector3.UnitZ, Vector3.UnitY, (float) _resolution.X / _resolution.Y);
    }

    private void OnRender(double delta)
    {
        // Delegate screen clear to renderer
        // _renderer.ClearScreen(0.1f, 0.1f, 0.1f, 1.0f);
        _renderer.ClearScreen(0.0f, 0.0f, 0.0f, 1.0f);
        
        // Delegate rendering to the scene
        _currentScene.Render(_renderer);
    }

    private void OnUpdate(double delta)
    {
        if (_inputManager.IsKeyPressed(Key.Escape))
        {
            _window.Close();
        }
        
        _currentScene.OnUpdate((float) delta);
        
        _inputManager.Clear();
    }

    public void Run() => _window.Run();
}