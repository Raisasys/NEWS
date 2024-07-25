using System.Runtime.CompilerServices;

namespace Core;

public struct DisposableAwaitable<T> where T : IDisposable
{
    public readonly Task<T> Task;

    internal DisposableAwaitable(Task<T> task) => Task = task;

    public TaskAwaiter<T> GetAwaiter() => Task.GetAwaiter();
}