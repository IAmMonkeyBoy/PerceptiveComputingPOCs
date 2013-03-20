using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace PCSDKAttributeDetection.Perceptual
{
    public abstract class AsyncPipelineBase : UtilMPipeline
    {
        private static readonly object SleepSyncLock = new object();
        private readonly object SyncLock = new object();

        protected void Sleep(int time)
        {
            lock (SleepSyncLock)
            {
                Monitor.Wait(SleepSyncLock, time);
            }
        }




        public async void Run()
        {
            try
            {
                await Task.Run(() =>
                {
                    while (true)
                    {
                        this.LoopFrames();
                        lock (SyncLock)
                        {
                            Monitor.Wait(SyncLock, 1000);
                        }
                    }
                });
            }
            finally
            {
                this.Dispose();
            }
        }
    }
}