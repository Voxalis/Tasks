namespace Voxalis.Tasks
{
    /// <summary>
    /// Task wait extensions.
    /// </summary>
    public static class TaskWaitExtensions
    {
        /// <summary>
        /// Wait the specified builder and duration.
        /// </summary>
        /// <returns>The wait.</returns>
        /// <param name="builder">Builder.</param>
        /// <param name="duration">Duration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static TaskBuilder<T> Wait<T>
        (
            this TaskBuilder<T> builder,
            float duration
        )
        {
            return builder.Push(new TaskWait<T>(duration));
        }

        /// <summary>
        /// Wait the specified builder, min and max.
        /// </summary>
        /// <returns>The wait.</returns>
        /// <param name="builder">Builder.</param>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Max.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static TaskBuilder<T> Wait<T>
        (
            this TaskBuilder<T> builder,
            float min,
            float max
        )
        {
            return builder.Push(new TaskWait<T>(min, max));
        }
    }
}
