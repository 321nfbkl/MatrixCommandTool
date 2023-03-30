using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixCommandTool.Helper
{
    /// <summary>
    /// 延迟加载
    /// </summary>
    public class DelayTrigger
    {
        public DelayTrigger(int delayTime = 500)
        {
            if (delayTime < 500)
                delayTime = 500;
            this.delayTime = delayTime;
        }
        /// <summary>
        /// 延迟执行时间(ms)
        /// </summary>
        private int delayTime;

        /// <summary>
        /// 关键字
        /// </summary>
        private string currentKey;

        /// <summary>
        /// 是否可以执行
        /// </summary>
        private volatile bool canExecute;

        /// <summary>
        /// 是否正在延迟响应中
        /// </summary>
        private volatile bool isDelaying;


        public Action<string> OnExecute;


        public void SetKey(string key)
        {
            currentKey = key;
            if (isDelaying)
            {
                canExecute = false;
                return;
            }
            Task.Factory.StartNew(delegate
            {
                isDelaying = true;
                while (true)
                {
                    canExecute = true;
                    Thread.Sleep(delayTime);
                    if (canExecute)
                    {
                        OnExecute?.Invoke(currentKey);
                        isDelaying = false;
                        break;
                    }
                }
            });
        }
    }
}
