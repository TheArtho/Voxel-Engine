using Silk.NET.OpenGL;

namespace VoxelEngine.Rendering.Shaders
{
    public class Shader
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

            // Vérification des erreurs
            var infoLog = _gl.GetShaderInfoLog(shader);
            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception($"Error of type \"{type}\" : {infoLog}");
            }

            return shader;
        }

        public void Use() => _gl.UseProgram(_programId);
    }
}