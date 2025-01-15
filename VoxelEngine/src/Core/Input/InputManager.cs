using Silk.NET.Input;
using Silk.NET.Windowing;

namespace VoxelEngine.Core.Input
{
    public class InputManager : IInputManager
    {
        private IInputContext? _inputContext;
        private IKeyboard? _keyboard;

        private readonly HashSet<Key> _keysPressed = [];
        private readonly HashSet<Key> _keysReleased = [];

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
        }

        private void OnKeyUp(IKeyboard sender, Key key, int scancode)
        {
            _keysPressed.Remove(key);
            _keysReleased.Add(key);
        }

        public bool IsKeyPressed(Key key) => _keysPressed.Contains(key);

        public bool IsKeyReleased(Key key)
        {
            if (!_keysReleased.Contains(key)) return false;
            
            _keysReleased.Remove(key); // Removes the event
            return true;

        }

        public void Update()
        {
            _keysReleased.Clear();
        }
    }
}