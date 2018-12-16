namespace Voxalis.Tasks
{
    /// <summary>
    /// Task composite.
    /// </summary>
    public interface ITaskComposite<TContext> : ITask<TContext>
    {
        /// <summary>
        /// Add the specified task.
        /// </summary>
        /// <param name="task">Task.</param>
        void Add(ITask<TContext> task);
    }
}
