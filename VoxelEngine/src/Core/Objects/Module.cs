namespace VoxelEngine.Core.Objects
{
    public abstract class Module : Object
    {
        public GameObject GameObject { get; internal set; } = null!;

        /// <summary>
        /// Method called when an object is initialized
        /// </summary>
        public virtual void OnInit() {}

        /// <summary>
        /// Method called on each frame
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void OnUpdate(float deltaTime) { }

        /// Method called when destroyed
        public virtual void OnDestroy() { }
    }
}