using System.Numerics;
using Silk.NET.OpenGL;

namespace VoxelEngine.Rendering.Shaders
{
    public class Shader: IDisposable
    {
        private readonly GL _gl;
        private readonly uint _programId;

        public Shader(GL gl, string vertexPath, string fragmentPath)
        {
            _gl = gl;
            _programId = _gl.CreateProgram();

            var vertexShader = LoadShader(vertexPath, ShaderType.VertexShader);
            var fragmentShader = LoadShader(fragmentPath, ShaderType.FragmentShader);

            _gl.AttachShader(_programId, vertexShader);
            _gl.AttachShader(_programId, fragmentShader);
            _gl.LinkProgram(_programId);

            _gl.DeleteShader(vertexShader);
            _gl.DeleteShader(fragmentShader);
        }

        private uint LoadShader(string path, ShaderType type)
        {
            var shader = _gl.CreateShader(type);
            var source = File.ReadAllText(path);
            _gl.ShaderSource(shader, source);
            _gl.CompileShader(shader);

            // Verify errors
            var infoLog = _gl.GetShaderInfoLog(shader);
            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception($"Error of type \"{type}\" : {infoLog}");
            }

            return shader;
        }

        public void Use() => _gl.UseProgram(_programId);
        
        // Set uniform for an int
        public void SetUniform(string name, int value)
        {
            var location = _gl.GetUniformLocation(_programId, name);
            if (location == -1)
            {
                throw new Exception($"Uniform \"{name}\" not found in shader.");
            }
            _gl.Uniform1(location, value);
        }

        // Set uniform for a float
        public void SetUniform(string name, float value)
        {
            var location = _gl.GetUniformLocation(_programId, name);
            if (location == -1)
            {
                throw new Exception($"Uniform \"{name}\" not found in shader.");
            }
            _gl.Uniform1(location, value);
        }

        // Set uniform for a Vector2
        public void SetUniform(string name, Vector2 value)
        {
            var location = _gl.GetUniformLocation(_programId, name);
            if (location == -1)
            {
                throw new Exception($"Uniform \"{name}\" not found in shader.");
            }
            _gl.Uniform2(location, value.X, value.Y);
        }

        // Set uniform for a Vector3
        public void SetUniform(string name, Vector3 value)
        {
            var location = _gl.GetUniformLocation(_programId, name);
            if (location == -1)
            {
                throw new Exception($"Uniform \"{name}\" not found in shader.");
            }
            _gl.Uniform3(location, value.X, value.Y, value.Z);
        }

        // Set uniform for a Vector4
        public void SetUniform(string name, Vector4 value)
        {
            var location = _gl.GetUniformLocation(_programId, name);
            if (location == -1)
            {
                throw new Exception($"Uniform \"{name}\" not found in shader.");
            }
            _gl.Uniform4(location, value.X, value.Y, value.Z, value.W);
        }
        
        // Set uniform for a 4x4 Matrix
        public void SetUniform(string name, Matrix4x4 value)
        {
            var location = _gl.GetUniformLocation(_programId, name);
            if (location == -1)
            {
                throw new Exception($"Uniform \"{name}\" not found in shader.");
            }

            // Create a 16-element float array and populate it with the matrix values in column-major order
            float[] matrixArray =
            [
                value.M11, value.M21, value.M31, value.M41,
                value.M12, value.M22, value.M32, value.M42,
                value.M13, value.M23, value.M33, value.M43,
                value.M14, value.M24, value.M34, value.M44
            ];

            // Pass the array to UniformMatrix4
            _gl.UniformMatrix4(location, 1, false, matrixArray);
        }

        public void Dispose()
        {
            _gl.DeleteProgram(_programId);
        }
    }
}