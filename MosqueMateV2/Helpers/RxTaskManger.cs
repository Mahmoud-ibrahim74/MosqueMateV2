using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace MosqueMateV2.Helpers
{
    public class RxTaskManger
    {
        /// <summary>
        /// Starts a periodic task using Rx.NET.
        /// </summary>
        /// <param name="taskToRun">The task to execute periodically.</param>
        /// <param name="interval">The interval between task executions.</param>
        public void StartTaskScheduler(Func<Task> taskToRun, TimeSpan interval)
        {
             Observable
            .Interval(interval) 
            .Select(_ => Observable.FromAsync(taskToRun)) 
            .Concat() 
            .Subscribe(
                _ => { /* Task completed successfully */ },
                ex => Console.WriteLine($"Task encountered an error: {ex.Message}")
            );
        }
        /// <summary>
        /// Starts a periodic task using Rx.NET that allows UI access.
        /// </summary>
        /// <param name="taskToRun">The task to execute periodically.</param>
        /// <param name="interval">The interval between task executions.</param>
        /// <param name="uiUpdateAction">An action to execute on the UI thread.</param>
        public void StartUITaskScheduler(Func<Task> taskToRun, TimeSpan interval, Action uiUpdateAction)
        {
                Observable
                .Interval(interval) 
                .Select(_ => Observable.FromAsync(async () =>
                {
                    await taskToRun();
                    Application.Current.Dispatcher.Invoke(uiUpdateAction); 
                }))
                .Concat() 
                .Subscribe(
                    _ => { /* Task completed successfully */ },
                    ex => Console.WriteLine($"Task encountered an error: {ex.Message}")
                );
        }

        /// <summary>
        /// Executes an asynchronous task in the background and processes its result on the UI thread.
        /// </summary>
        /// <typeparam name="T">The type of the result returned by the asynchronous task.</typeparam>
        /// <param name="backgroundTask">A function that represents the asynchronous task to be executed in the background.</param>
        /// <param name="retryNumber">retries the task up to the specified number of times (retryCount)</param>
        /// <param name="onSuccess">An action to handle the result of the task on the UI thread upon successful completion.</param>
        /// <param name="onError">An action to handle the result of the task if has exception .</param>
        /// <param name="onCancelled">An action to cancel current task running on background.</param>
        /// <remarks>
        /// This method uses the System.Reactive library to handle asynchronous task execution and ensures that 
        /// the success callback is invoked on the UI thread.
        /// Any errors encountered during task execution are logged to the console.
        /// </remarks>
        public void RunBackgroundTaskOnUI<T>(
            Func<CancellationToken, Task<T>> backgroundTask,
            Action<T> onSuccess,
            int retryNumber = 3,
            Action onError = null,
            Action onCancelled = null)
        {
            var uiScheduler = new SynchronizationContextScheduler(SynchronizationContext.Current);
            var cancellationTokenSource = new CancellationTokenSource();

            Observable
                .FromAsync(() => backgroundTask(cancellationTokenSource.Token))
                .Retry(retryNumber) // Retry on error
                .ObserveOn(uiScheduler) // Switch to UI thread
                .Subscribe(
                    result => onSuccess(result), // Handle success
                    error =>
                    {
                        // Handle errors separately
                        if (error is OperationCanceledException)
                        {
                            onCancelled?.Invoke(); // User requested cancellation
                        }
                        else
                        {
                            Console.WriteLine(error.Message); // Log error
                            onError?.Invoke(); // Handle other errors
                        }
                    });

            // Store the cancellation token source for external control
            _cancellationTokenSource = cancellationTokenSource;
        }
        public CancellationTokenSource _cancellationTokenSource;

        // Method to allow user cancellation
        public void StopBackgroundTask()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel(); // Signal cancellation
                _cancellationTokenSource.Dispose(); // Clean up
                _cancellationTokenSource = null; // Prevent reuse
            }
        }
    }
}
