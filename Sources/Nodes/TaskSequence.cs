using System.Linq;
using System.Collections.Generic;

namespace Voxalis.Tasks
{
    /// <summary>
    /// Task sequence.
    /// </summary>
    public class TaskSequence<TContext> : ITaskComposite<TContext>
    {
        /// <summary>
        /// The tasks.
        /// </summary>
        private readonly List<ITask<TContext>> Tasks = new List<ITask<TContext>>();

        /// <summary>
        /// The current.
        /// </summary>
        private ITask<TContext> Task;

        /// <summary>
        /// Add the specified task.
        /// </summary>
        /// <param name="task">Task.</param>
        public void Add(ITask<TContext> task) => Tasks.Add(task);

        /// <summary>
        /// Tick the specified context.
        /// </summary>
        /// <returns>The tick.</returns>
        /// <param name="context">Context.</param>
        public TaskStatus Tick(TContext context)
        {
            var query = Task == null ? Tasks : Tasks.SkipWhile(t => t != Task);

            return query.Aggregate(TaskStatus.Success, (acc, curr) =>
            {
                if (acc == TaskStatus.Success)
                {
                    var result = curr.Tick(context);
                    Task = result == TaskStatus.Running ? curr : null;
                    return result;
                }
                return acc;
            });
        }
    }
}
