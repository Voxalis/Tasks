namespace Voxalis.Tasks
{
    /// <summary>
    /// Task condition extensions.
    /// </summary>
    public static class TaskConditionExtensions
    {
        /// <summary>
        /// Condition the specified builder and function.
        /// </summary>
        /// <returns>The condition.</returns>
        /// <param name="builder">Builder.</param>
        /// <param name="function">Function.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static TaskBuilder<T> Condition<T>
        (
            this TaskBuilder<T> builder,
            TaskCondition<T>.Delegate function
        )
        {
            return builder.Push(new TaskCondition<T>(function));
        }
    }
}
