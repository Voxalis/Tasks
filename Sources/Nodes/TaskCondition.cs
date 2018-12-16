namespace Voxalis.Tasks
{
    /// <summary>
    /// Task condition.
    /// </summary>
    public class TaskCondition<TContext> : ITask<TContext>
    {
        /// <summary>
        /// Delegate.
        /// </summary>
        public delegate bool Delegate();

        /// <summary>
        /// The function.
        /// </summary>
        private Delegate Function { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Voxalis.Tasks.Nodes.TaskCondition`1"/> class.
        /// </summary>
        /// <param name="function">Function.</param>
        public TaskCondition(Delegate function)
        {
            Function = function;
        }

        /// <summary>
        /// Tick the specified context.
        /// </summary>
        /// <returns>The tick.</returns>
        /// <param name="context">Context.</param>
        public TaskStatus Tick(TContext context)
        {
            return Function.Invoke() ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
