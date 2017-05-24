using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelBfs.Sdk.Alghorithms
{
    public class TaskRunner
    {
        private Task task;
        private bool work;
        private Queue<Action> actionCueue;

        public TaskRunner()
        {
            this.work = true;
            this.actionCueue = new Queue<Action>();
            this.task = new Task(() =>
            {
                while (this.work)
                {
                    if (this.actionCueue.Count > 0)
                    {
                        this.Working = true;

                        Action act = this.actionCueue.Dequeue();
                        act();

                        this.Working = false;
                    }
                }
            });
            this.task.Start();
        }

        public bool Working { get; private set; }

        public void Stop()
        {
            this.work = false;
        }

        public void ExecuteAction(Action act)
        {
            this.actionCueue.Enqueue(act);
        }
    }
}
