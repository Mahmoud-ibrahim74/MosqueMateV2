using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace MosqueMateV2.Helpers.AppHelpers
{
    /// <summary>
    /// Provides utility methods for running asynchronous tasks in the background and handling their results on the UI thread.
    /// </summary>
    public class TaskHelper
    {
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
        public static void RunBackgroundTaskOnUI<T>(
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
        /// Executes an asynchronous task in the background and ensures control returns to the UI thread upon completion.
        /// </summary>
        /// <param name="backgroundTask">A function that represents the asynchronous task to be executed in the background.</param>
        /// <param name="onCompleted">An action to be executed on the UI thread upon successful completion of the task.</param>
        /// <remarks>
        /// This method is designed for tasks that do not return a result. 
        /// It ensures that any completion logic is executed on the UI thread.
        /// Errors encountered during task execution are logged to the console.
        /// </remarks>
        public static void RunBackgroundTaskOnUI(
            Func<Task> backgroundTask,
            Action onCompleted)
        {
            var uiScheduler = new SynchronizationContextScheduler(SynchronizationContext.Current);
            Observable
                .FromAsync(backgroundTask) // Run the asynchronous task
                .ObserveOn(uiScheduler)    // Switch back to the UI thread
                .Subscribe(
                    _ => onCompleted(), // Handle successful completion
                    error =>
                    {
                        Console.WriteLine($"Error: {error.Message}");
                    });
        }
    }
}
