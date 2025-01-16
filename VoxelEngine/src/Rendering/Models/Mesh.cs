using Silk.NET.OpenGL;
using System;
using System.Runtime.InteropServices;
using Assimp;
using PrimitiveType = Silk.NET.OpenGL.PrimitiveType;

namespace VoxelEngine.Rendering.Models
{
    public class Mesh : IDisposable
    {
        private readonly GL _gl;

        // OpenGL Buffers and VAO
        private uint _vao;
        private uint _vbo;
        private uint _ebo;

        // Vertex and Index Data
        private float[] _vertices;
        private uint[] _indices;

        public Mesh(GL gl, float[] vertices, uint[] indices)
        {
            _gl = gl;

            _vertices = vertices;
            _indices = indices;

            // Initialize OpenGL buffers
            InitializeBuffers();
        }

        private void InitializeBuffers()
        {
            // Create Vertex Array Object (VAO)
            _vao = _gl.GenVertexArray();
            _gl.BindVertexArray(_vao);
            
            // Create Vertex Buffer Object (VBO)
            _vbo = _gl.GenBuffer();
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
            unsafe
            {
                fixed (float* vertexPtr = _vertices)
                {
                    _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(_vertices.Length * sizeof(float)), (IntPtr)vertexPtr, BufferUsageARB.StaticDraw);
                }
            }

            // Create Element Buffer Object (EBO)
            _ebo = _gl.GenBuffer();
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);
            unsafe
            {
                fixed (uint* indexPtr = _indices)
                {
                    _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(_indices.Length * sizeof(uint)), (IntPtr)indexPtr, BufferUsageARB.StaticDraw);
                }
            }
            // Configure vertex attributes (assume position only for simplicity)
            _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            _gl.EnableVertexAttribArray(0);

            // Unbind the VAO to prevent accidental modifications
            _gl.BindVertexArray(0);
        }

        public void Draw()
        {
            // Bind the VAO and draw the mesh
            _gl.BindVertexArray(_vao);
            unsafe
            {
                _gl.DrawElements(PrimitiveType.Triangles, (uint)_indices.Length, DrawElementsType.UnsignedInt, null);
            }
            _gl.BindVertexArray(0);
        }

        public void Dispose()
        {
            // Clean up GPU resources
            _gl.DeleteBuffer(_vbo);
            _gl.DeleteBuffer(_ebo);
            _gl.DeleteVertexArray(_vao);
        }

        /// <summary>
        /// Load a model file from a path
        /// </summary>
        /// <param name="gl">GL context</param>
        /// <param name="filePath">Absolute path of the file</param>
        /// <returns>Generated mesh</returns>
        /// <exception cref="Exception"></exception>
        public static Mesh LoadFromFile(GL gl, string filePath)
        {
            var context = new AssimpContext();

            // Load the file
            var scene = context.ImportFile(filePath, PostProcessSteps.Triangulate /* | PostProcessSteps.GenerateSmoothNormals */);

            if (scene is not { HasMeshes: true })
                throw new Exception($"Failed to load mesh from file: {filePath}");

            // Take the first mesh
            var mesh = scene.Meshes[0];

            // Collect vertex data (position, normal, uv)
            var vertices = new List<float>();
            for (int i = 0; i < mesh.Vertices.Count; i++)
            {
                var vertex = mesh.Vertices[i];
                var normal = mesh.HasNormals ? mesh.Normals[i] : new Assimp.Vector3D(0, 0, 0);
                var uv = mesh.HasTextureCoords(0) ? mesh.TextureCoordinateChannels[0][i] : new Assimp.Vector3D(0, 0, 0);

                vertices.Add(vertex.X);
                vertices.Add(vertex.Y);
                vertices.Add(vertex.Z);
                vertices.Add(normal.X);
                vertices.Add(normal.Y);
                vertices.Add(normal.Z);
                vertices.Add(uv.X);
                vertices.Add(uv.Y);
            }

            // Collect index data
            var indices = new List<uint>();
            foreach (var face in mesh.Faces.Where(face => face.IndexCount == 3))
            {
                indices.Add((uint)face.Indices[0]);
                indices.Add((uint)face.Indices[1]);
                indices.Add((uint)face.Indices[2]);
            }

            return new Mesh(gl, vertices.ToArray(), indices.ToArray());
        }

        public static Mesh CreateTriangle(GL gl)
        {
            float[] vertices =
            {
                // Positions
                0.0f,  0.5f, 0.0f, // Top
               -0.5f, -0.5f, 0.0f, // Bottom Left
                0.5f, -0.5f, 0.0f  // Bottom Right
            };

            uint[] indices =
            {
                0, 1, 2 // Single triangle
            };

            return new Mesh(gl, vertices, indices);
        }

        public static Mesh CreateQuad(GL gl)
        {
            float[] vertices =
            {
                // Positions
               -0.5f,  0.5f, 0.0f, // Top Left
               -0.5f, -0.5f, 0.0f, // Bottom Left
                0.5f, -0.5f, 0.0f, // Bottom Right
                0.5f,  0.5f, 0.0f  // Top Right
            };

            uint[] indices =
            {
                0, 1, 2, // First triangle
                2, 3, 0  // Second triangle
            };

            return new Mesh(gl, vertices, indices);
        }
    }
}
