namespace Voxalis.Tasks
{
    /// <summary>
    /// Task action.
    /// </summary>
    public class TaskAction<TContext> : ITask<TContext>
    {
        /// <summary>
        /// Delegate.
        /// </summary>
        public delegate TaskStatus Delegate(TContext context);

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        private Delegate Action { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Voxalis.Tasks.Nodes.TaskAction`1"/> class.
        /// </summary>
        /// <param name="action">Action.</param>
        public TaskAction(Delegate action)
        {
            Action = action;
        }

        /// <summary>
        /// Tick the specified context.
        /// </summary>
        /// <returns>The tick.</returns>
        /// <param name="context">Context.</param>
        public TaskStatus Tick(TContext context)
        {
            return Action.Invoke(context);
        }
    }
}
