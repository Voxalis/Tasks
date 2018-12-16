using System.Collections.Generic;

namespace Voxalis.Tasks
{
    /// <summary>
    /// Task parallel.
    /// </summary>
    public class TaskParallel<TContext> : ITaskComposite<TContext>
    {
        /// <summary>
        /// The tasks.
        /// </summary>
        private readonly List<ITask<TContext>> Tasks = new List<ITask<TContext>>();

        /// <summary>
        /// Number of child failures required to terminate with failure.
        /// </summary>
        private readonly int NumRequiredToFail;

        /// <summary>
        /// Number of child successess require to terminate with success.
        /// </summary>
        private readonly int NumRequiredToSucceed;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Voxalis.Tasks.TaskParallel`1"/> class.
        /// </summary>
        /// <param name="numRequiredToFail">Number required to fail.</param>
        /// <param name="numRequiredToSucceed">Number required to succeed.</param>
        public TaskParallel(int numRequiredToFail = 1, int numRequiredToSucceed = 1)
        {
            NumRequiredToFail = numRequiredToFail;
            NumRequiredToSucceed = numRequiredToSucceed;
        }

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
            var numChildrenSuceeded = 0;
            var numChildrenFailed = 0;

            foreach (var child in Tasks)
            {
                var childStatus = child.Tick(context);

                switch (childStatus)
                {
                    case TaskStatus.Success: ++numChildrenSuceeded; break;
                    case TaskStatus.Failure: ++numChildrenFailed; break;
                }
            }

            if (NumRequiredToSucceed > 0 && numChildrenSuceeded >= NumRequiredToSucceed)
            {
                return TaskStatus.Success;
            }

            if (NumRequiredToFail > 0 && numChildrenFailed >= NumRequiredToFail)
            {
                return TaskStatus.Failure;
            }

            return TaskStatus.Running;
        }
    }
}
