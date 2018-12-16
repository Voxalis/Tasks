namespace Voxalis.Tasks
{
    /// <summary>
    /// Task selector extensions.
    /// </summary>
    public static class TaskSelectorExtensions
    {
        /// <summary>
        /// Selector the specified builder.
        /// </summary>
        /// <returns>The selector.</returns>
        /// <param name="builder">Builder.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static TaskBuilder<T> Selector<T>(this TaskBuilder<T> builder)
        {
            return builder.Push(new TaskSelector<T>());
        }
    }
}
