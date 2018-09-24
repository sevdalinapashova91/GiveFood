using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GiveFoodInfrastructure.BackgroudTask
{
    public class BackgroundTask : IBackgroundTask
    {
        public string EnqueueOneTimeJob(Expression<Action> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }
        public string EnqueueOneTimeJob<T>(Expression<Action<T>> methodCall)
        {
            return BackgroundJob.Enqueue<T>(methodCall);
        }
    }
}
