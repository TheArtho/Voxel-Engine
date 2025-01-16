using Silk.NET.OpenGL;
using VoxelEngine.Rendering.Models;
using Shader = VoxelEngine.Rendering.Shaders.Shader;

namespace VoxelEngine.Rendering
{
    public class Renderer(GL gl)
    {
        public void ClearScreen(float r, float g, float b, float a)
        {
            gl.ClearColor(r, g, b, a);
            gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void DrawMesh(Mesh mesh, Shader shader)
        {
            
        }
    }
}