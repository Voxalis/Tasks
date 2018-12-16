using System;

namespace Voxalis.Tasks
{
    /// <summary>
    /// Task wait.
    /// </summary>
    public class TaskWait<TContext> : ITask<TContext>
    {
        /// <summary>
        /// The values.
        /// </summary>
        private readonly float[] Values;

        /// <summary>
        /// The start.
        /// </summary>
        private DateTime ExpectedTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Voxalis.Tasks.TaskWait`1"/> need reset.
        /// </summary>
        /// <value><c>true</c> if need reset; otherwise, <c>false</c>.</value>
        private bool NeedReset { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Voxalis.Tasks.TaskWait`1"/> class.
        /// </summary>
        /// <param name="duration">Duration.</param>
        public TaskWait(float duration)
        {
            Values = new float[] { duration };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Voxalis.Tasks.TaskWait`1"/> class.
        /// </summary>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Max.</param>
        public TaskWait(float min, float max)
        {
            Values = new float[] { min, max };
        }

        /// <summary>
        /// Tick the specified context.
        /// </summary>
        /// <returns>The tick.</returns>
        /// <param name="context">Context.</param>
        public TaskStatus Tick(TContext context)
        {
            if (NeedReset)
            {
                ExpectedTime = Values.Length == 2
                    ? DateTime.Now + TimeSpan.FromSeconds(
                        UnityEngine.Random.Range(Values[0], Values[1])
                    )
                    : DateTime.Now + TimeSpan.FromSeconds(Values[0]);

                NeedReset = false;
            }

            if (DateTime.Now < ExpectedTime)
            {
                return TaskStatus.Running;
            }

            NeedReset = true;

            return TaskStatus.Success;
        }
    }
}
