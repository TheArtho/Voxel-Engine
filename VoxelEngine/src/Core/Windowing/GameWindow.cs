using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using VoxelEngine.AssetManagement;
using VoxelEngine.Core.Input;
using VoxelEngine.Rendering;
using VoxelEngine.SceneManagement;
using Shader = VoxelEngine.Rendering.Shaders.Shader;

namespace VoxelEngine.Core.Windowing;

public class GameWindow
{
    private readonly IWindow _window;
    private InputManager _inputManager = null!;
    private GL _gl = null!;
    
    private Renderer _renderer = null!;
    private Scene _currentScene = null!;
    
    private AssetManager _assetManager;

    public GameWindow()
    {
        var options = WindowOptions.Default;
        options.Size = new Silk.NET.Maths.Vector2D<int>(1280, 720);
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
    }

    private void OnRender(double delta)
    {
        // Delegate screen clear to renderer
        _renderer.ClearScreen(0.1f, 0.1f, 0.1f, 1.0f);
        
        // Delegate rendering to the scene
        _currentScene.Render(_renderer);
    }

    private void OnUpdate(double delta)
    {
        if (_inputManager.IsKeyPressed(Key.Escape))
        {
            _window.Close();
        }
        
        _inputManager.Clear();
    }

    public void Run() => _window.Run();
}