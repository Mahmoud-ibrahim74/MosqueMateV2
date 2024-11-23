using System.Reactive.Linq;

namespace MosqueMateV2.Helpers
{
    public class RxTaskScheduler : IDisposable
    {
        private IDisposable _taskSubscription;

        /// <summary>
        /// Starts a periodic task using Rx.NET.
        /// </summary>
        /// <param name="taskToRun">The task to execute periodically.</param>
        /// <param name="interval">The interval between task executions.</param>
        public void Start(Func<Task> taskToRun, TimeSpan interval)
        {
            if (_taskSubscription != null)
                throw new InvalidOperationException("Task scheduler is already running.");


                _taskSubscription = Observable
                .Interval(interval) // Emit values at the specified interval
                .Select(_ => Observable.FromAsync(taskToRun)) // Convert the task to an observable
                .Concat() // Ensure tasks run sequentially (wait for one to finish before starting the next)
                .Subscribe(
                    _ => { /* Task completed successfully */ },
                    ex => Console.WriteLine($"Task encountered an error: {ex.Message}")
                );
        }

        /// <summary>
        /// Stops the running periodic task.
        /// </summary>
        public void Stop()
        {
            _taskSubscription?.Dispose();
            _taskSubscription = null;
        }

        public void Dispose() => Stop();
    }
}
