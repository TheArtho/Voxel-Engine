using Silk.NET.Input;
using Silk.NET.Windowing;

namespace VoxelEngine.Core.Input
{
    public class InputManager : IInputManager
    {
        public static InputManager Instance { get; private set; }
        
        private IInputContext? _inputContext;
        private IKeyboard? _keyboard;

        private readonly HashSet<Key> _keysDown = [];
        private readonly HashSet<Key> _keysPressed = [];
        private readonly HashSet<Key> _keysReleased = [];

        public InputManager()
        {
            Instance = this;
        }

        public void Initialize(IWindow window)
        {
            _inputContext = window.CreateInput();
            if (_inputContext.Keyboards.Count > 0)
            {
                _keyboard = _inputContext.Keyboards[0];
                _keyboard.KeyDown += OnKeyDown;
                _keyboard.KeyUp += OnKeyUp;
            }
        }

        private void OnKeyDown(IKeyboard sender, Key key, int scancode)
        {
            _keysPressed.Add(key);
            _keysDown.Add(key);
        }

        private void OnKeyUp(IKeyboard sender, Key key, int scancode)
        {
            _keysDown.Remove(key);
            _keysReleased.Add(key);
        }

        public bool IsKeyDown(Key key) => _keysDown.Contains(key);
        
        public bool IsKeyPressed(Key key) => _keysPressed.Contains(key);

        public bool IsKeyReleased(Key key)
        {
            if (!_keysReleased.Contains(key)) return false;
            
            _keysReleased.Remove(key); // Removes the event
            return true;

        }

        public void Clear()
        {
            _keysPressed.Clear();
            _keysReleased.Clear();
        }
    }
}