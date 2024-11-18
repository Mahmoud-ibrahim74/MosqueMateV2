using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace MosqueMateV2.Helpers.AppHelpers
{
    public class TaskHelper
    {
        public static void RunBackgroundTaskOnUI<T>(
            Func<Task<T>> backgroundTask,
            Action<T> onSuccess)
        {
            var uiScheduler = new SynchronizationContextScheduler(SynchronizationContext.Current);

            Observable
                .FromAsync(backgroundTask) // Run the asynchronous task
                .ObserveOn(uiScheduler)    // Switch back to the UI thread
                .Subscribe(
                    result => onSuccess(result), // Handle success
                     error =>
                        {
                            Console.WriteLine($"Error: {error.Message}");
                        });          
        }
    }
}
