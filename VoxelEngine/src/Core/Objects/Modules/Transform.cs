using System.Numerics;

namespace VoxelEngine.Core.Objects.Modules
{
    public class Transform : Module
    {
        /// <summary>
        /// Parent transform
        /// </summary>
        public Transform? Parent = null;
        
        /// <summary>
        /// Position of the object
        /// </summary>
        public Vector3 Position { get; set; } = Vector3.Zero;
        /// <summary>
        /// Rotation of the object in degrees (Euler angles)
        /// </summary>
        public Vector3 Rotation { get; set; } = Vector3.Zero;
        /// <summary>
        /// Scale of the object
        /// </summary>
        public Vector3 Scale { get; set; } = Vector3.One;

        public Matrix4x4 GetTransformationMatrix()
        {
            var translation = Matrix4x4.CreateTranslation(Position);
            var rotationX = Matrix4x4.CreateRotationX(Rotation.X * MathF.PI / 180);
            var rotationY = Matrix4x4.CreateRotationY(Rotation.Y * MathF.PI / 180);
            var rotationZ = Matrix4x4.CreateRotationZ(Rotation.Z * MathF.PI / 180);
            var scaling = Matrix4x4.CreateScale(Scale);
            
            // TODO Apply parent transform from GameObject.Parent

            return scaling * (rotationZ * rotationY * rotationX) * translation;
        }
    }
}