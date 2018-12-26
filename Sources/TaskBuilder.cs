using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxalis.Tasks
{
    /// <summary>
    /// Task builder.
    /// </summary>
    public static class TaskBuilder
    {
        /// <summary>
        /// Create the specified context.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="context">Context.</param>
        /// <typeparam name="TContext">The 1st type parameter.</typeparam>
        public static TaskBuilder<TContext> Create<TContext>(TContext context)
        {
            return new TaskBuilder<TContext>(context);
        }
    }

    /// <summary>
    /// Task builder.
    /// </summary>
    public sealed class TaskBuilder<TContext>
    {
        /// <summary>
        /// The context.
        /// </summary>
        public readonly TContext Context;

        /// <summary>
        /// The stack.
        /// </summary>
        private readonly Stack<ITask<TContext>> Stack = new Stack<ITask<TContext>>();

        /// <summary>
        /// Gets or sets the current task.
        /// </summary>
        /// <value>The current task.</value>
        private ITask<TContext> CurrentTask { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Voxalis.Tasks.TaskBuilder`1"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public TaskBuilder(TContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Push the specified task.
        /// </summary>
        /// <returns>The push.</returns>
        /// <param name="task">Task.</param>
        public TaskBuilder<TContext> Push(ITask<TContext> task)
        {
            if (task == null)
            {
                throw new ArgumentNullException($"Cant' add an empty {nameof(task)}");
            }

            if (task is ITaskComposite<TContext>)
            {
                if (Stack.Count > 0)
                {
                    ((ITaskComposite<TContext>)Stack.Peek()).Add(task);
                }

                Stack.Push(task);
            }
            else
            {
                if (Stack.Count <= 0)
                {
                    throw new ApplicationException("Can't create an unnested TaskAction, it must be an action.");
                }

                ((ITaskComposite<TContext>)Stack.Peek()).Add(task);
            }

            return this;
        }

        /// <summary>
        /// End this instance.
        /// </summary>
        /// <returns>The end.</returns>
        public TaskBuilder<TContext> End()
        {
            CurrentTask = Stack.Pop();
            return this;
        }

        /// <summary>
        /// Log the specified message.
        /// </summary>
        /// <returns>The log.</returns>
        /// <param name="message">Message.</param>
        public TaskBuilder<TContext> Log(string message)
        {
            return Push(new TaskAction<TContext>((context) =>
            {
                Debug.Log(message); return TaskStatus.Success;
            }));
        }

        /// <summary>
        /// Log the specified action.
        /// </summary>
        /// <returns>The log.</returns>
        /// <param name="action">Action.</param>
        public TaskBuilder<TContext> Log(Action action)
        {
            return Push(new TaskAction<TContext>((context) =>
            {
                action();
                return TaskStatus.Success;
            }));
        }

        /// <summary>
        /// Build this instance.
        /// </summary>
        /// <returns>The build.</returns>
        public ITask<TContext> Build()
        {
            if (CurrentTask == null)
            {
                throw new InvalidOperationException("Tree must contain at least one task");
            }

            return CurrentTask;
        }

        /// <summary>
        /// Tick this instance.
        /// </summary>
        public void Tick()
        {
            CurrentTask?.Tick(Context);
        }
    }
}
