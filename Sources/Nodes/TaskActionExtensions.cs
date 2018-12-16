namespace Voxalis.Tasks
{
    /// <summary>
    /// Task action extensions.
    /// </summary>
    public static class TaskActionExtensions
    {
        /// <summary>
        /// Action the specified builder and action.
        /// </summary>
        /// <returns>The action.</returns>
        /// <param name="builder">Builder.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static TaskBuilder<T> Action<T>
        (
            this TaskBuilder<T> builder,
            TaskAction<T>.Delegate action
        )
        {
            return builder.Push(new TaskAction<T>(action));
        }
    }
}
