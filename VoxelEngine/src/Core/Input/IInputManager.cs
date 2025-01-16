using Silk.NET.Input;
using Silk.NET.Windowing;

namespace VoxelEngine.Core.Input
{
    public interface IInputManager
    {
        void Initialize(IWindow window);
        bool IsKeyPressed(Key key);
        bool IsKeyReleased(Key key);
        void Clear();
    }
}