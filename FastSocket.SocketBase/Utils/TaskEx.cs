using System;
using System.Threading;
using System.Threading.Tasks;

namespace FastSocket.SocketBase.Utils
{
    /// <summary>
    /// Task任务的扩展方法
    /// </summary>
    static public class TaskEx
    {
        /// <summary>
        /// 延迟执行任务
        /// </summary>
        /// <param name="dueTime"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        static public Task Delay(int dueTime)
        {
            if (dueTime < -1) throw new ArgumentOutOfRangeException("dueTime");

            Timer timer = null;
            var source = new TaskCompletionSource<bool>();
            timer = new Timer(_ =>
            {
                using (timer) source.TrySetResult(true);
            },null,dueTime,Timeout.Infinite);

            return source.Task;
        }
    }
}