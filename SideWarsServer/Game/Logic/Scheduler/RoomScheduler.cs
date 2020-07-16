using SideWars.Shared.Utils;
using System;
using System.Collections.Generic;

namespace SideWarsServer.Game.Logic.Scheduler
{
    public class ScheduledLoopJob : ScheduledJob
    {
        public ScheduledLoopJob(int startTick, int executeEvery, Action action) : base(action, startTick)
        {
            ExecuteAt = startTick;
            ExecuteEvery = executeEvery;
            Action = action;
        }

        public int ExecuteEvery { get; set; }
        public int LastExecutedTick { get; set; } = -1;
    }

    public class ScheduledJob
    {
        public ScheduledJob(Action action, int executeAt)
        {
            Action = action;
            ExecuteAt = executeAt;
        }

        public Action Action { get; set; }

        /// <summary>
        /// Which tick will it execute the action
        /// </summary>
        public int ExecuteAt { get; set; }
    }

    public class RoomScheduler
    {
        private List<ScheduledJob> jobList = new List<ScheduledJob>();
        private List<ScheduledJob> completedJobs = new List<ScheduledJob>();
        private int currentTick;

        public void ScheduleJobAfter(Action job, int executeAfter)
        {
            jobList.Add(new ScheduledJob(job, currentTick + executeAfter));
        }

        public void ScheduleJob(Action job, int executeTick)
        {
            jobList.Add(new ScheduledJob(job, executeTick));
        }

        public void ScheduleJob(Action job, int startTick, int repeatTick)
        {
            jobList.Add(new ScheduledLoopJob(startTick, repeatTick, job));
        }

        public void Update(int currentTick)
        {
            this.currentTick = currentTick;
            
            UpdateJobs(currentTick);
            RemoveCompletedJobs();
        }

        void UpdateJobs(int currentTick)
        {
            foreach (var job in jobList)
            {
                if (job is ScheduledLoopJob)
                {
                    var loopJob = (ScheduledLoopJob)job;
                    if (loopJob.LastExecutedTick != -1 && loopJob.LastExecutedTick + loopJob.ExecuteEvery <= currentTick)
                    {
                        loopJob.Action();
                    }
                    else if (loopJob.LastExecutedTick == -1 && loopJob.ExecuteEvery <= currentTick)
                    {
                        loopJob.Action();
                    }
                }
                else
                {
                    if (job.ExecuteAt <= currentTick)
                    {
                        job.Action();
                        completedJobs.Add(job);
                    }
                }
            }
        }

        void RemoveCompletedJobs()
        {
            foreach (var job in completedJobs)
            {
                jobList.Remove(job);
            }

            completedJobs.Clear();
        }
    }
}
