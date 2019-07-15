using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetCat
{
    public class TimerTask
    {
        public TimerTask()
        {
            JobTaskCancellationTokenSource = new CancellationTokenSource();
            JobTask = Task.Run(() => OnTick(new EventArgs()), JobTaskCancellationTokenSource.Token);
        }

        /// <summary>
        /// 使用委托和时间间隔构造一个定时任务
        /// </summary>
        /// <param name="tick">委托任务</param>
        /// <param name="interval">时间间隔(毫秒)</param>
        public TimerTask(Func<Task> tick, int interval) : this()
        {
            Tick = tick;
            Interval = interval;
        }

        public bool IsStart { get; private set; }
        public int Interval { get; set; }
        public Func<Task> Tick { get; set; }

        private int JobInterval = int.MaxValue;
        private Task JobTask;
        private CancellationTokenSource JobTaskCancellationTokenSource;
        private CancellationTokenSource JobTaskDelayCancellationTokenSource;

        public void Start()
        {
            IsStart = true;
            JobInterval = Interval;
            JobTaskDelayCancellationTokenSource?.Cancel();
        }

        public void Stop()
        {
            IsStart = false;
            JobInterval = int.MaxValue;
        }
        public void Dispose()
        {
            Stop();
            JobTaskDelayCancellationTokenSource?.Cancel();
            JobTaskCancellationTokenSource.Cancel();
        }

        protected virtual async Task OnTick(EventArgs e)
        {
            while (true)
            {
                if (JobTask.IsCanceled)
                    break;

                JobTaskDelayCancellationTokenSource = new CancellationTokenSource();
                await Task.Delay(JobInterval, JobTaskDelayCancellationTokenSource.Token).ContinueWith(t =>
                {
                    if (t.Exception != null)
                        return;
                });

                if (IsStart)
                    await Tick?.Invoke();
            }
        }
    }
}
