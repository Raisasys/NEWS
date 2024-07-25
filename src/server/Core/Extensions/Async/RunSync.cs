namespace Core;

partial class XExtensions
{
    public static void RunSync(this TaskFactory @this, Func<Task> task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));

        @this.StartNew(task).Unwrap().GetAwaiter().GetResult();
    }

    public static void RunSyncOnNewThread(this TaskFactory @this, Func<Task> task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));

        @this.StartNew(task, TaskCreationOptions.LongRunning).Unwrap().GetAwaiter().GetResult();
    }

    public static TResult RunSync<TResult>(this TaskFactory @this, Func<Task<TResult>> task)
    {
        return @this.StartNew(task).Unwrap().GetAwaiter().GetResult();
    }

    public static TResult RunSyncOnNewThread<TResult>(this TaskFactory @this, Func<Task<TResult>> task)
    {
        return @this.StartNew(task, TaskCreationOptions.LongRunning).Unwrap().GetAwaiter().GetResult();
    }
}