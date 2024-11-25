using System.Reactive.Concurrency;
using System.Reactive.Linq;
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
        /// <remarks>
        /// This method uses the System.Reactive library to handle asynchronous task execution and ensures that 
        /// the success callback is invoked on the UI thread.
        /// Any errors encountered during task execution are logged to the console.
        /// </remarks>
        public  void RunBackgroundTaskOnUI<T>(
            Func<Task<T>> backgroundTask,
            Action<T> onSuccess,
            int retryNumber = 3,
            Action onError = null
            )
        {
            var uiScheduler = new SynchronizationContextScheduler(SynchronizationContext.Current);
            Observable
           .FromAsync(backgroundTask) // Run the asynchronous task
           .Retry(retryNumber)
           .ObserveOn(uiScheduler)    // Switch back to the UI thread
           .Subscribe(
               result => onSuccess(result), // Handle success
               error =>
               {
                   if (!string.IsNullOrEmpty(error.Message))
                   {
                       if (onError != null)
                           onError();
                   }
               });
        }


        /// <summary>
        /// Stops the running periodic task.
        /// </summary>

    }
}
