namespace Voxalis.Tasks
{
    /// <summary>
    /// Task parallel extensions.
    /// </summary>
    public static class TaskParallelExtensions
    {
        /// <summary>
        /// Parallel the specified builder, numRequiredToFail and numRequiredToSucced.
        /// </summary>
        /// <returns>The parallel.</returns>
        /// <param name="builder">Builder.</param>
        /// <param name="numRequiredToFail">Number required to fail.</param>
        /// <param name="numRequiredToSucced">Number required to succed.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static TaskBuilder<T> Parallel<T>
        (
            this TaskBuilder<T> builder,
            int numRequiredToFail = 1,
            int numRequiredToSucced = 1
        )
        {
            return builder.Push(new TaskParallel<T>(
                numRequiredToFail,
                numRequiredToSucced
            ));
        }
    }
}
