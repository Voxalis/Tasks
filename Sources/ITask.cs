namespace Voxalis.Tasks
{
    /// <summary>
    /// Task.
    /// </summary>
    public interface ITask<TContext>
    {
        /// <summary>
        /// Tick the specified context.
        /// </summary>
        /// <returns>The tick.</returns>
        /// <param name="context">Context.</param>
        TaskStatus Tick(TContext context);
    }
}
