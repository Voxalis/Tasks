namespace Voxalis.Tasks
{
    /// <summary>
    /// Task sequence extensions.
    /// </summary>
    public static class TaskSequenceExtensions
    {
        /// <summary>
        /// Sequence the specified builder.
        /// </summary>
        /// <returns>The sequence.</returns>
        /// <param name="builder">Builder.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static TaskBuilder<T> Sequence<T>(this TaskBuilder<T> builder)
        {
            return builder.Push(new TaskSequence<T>());
        }
    }
}
