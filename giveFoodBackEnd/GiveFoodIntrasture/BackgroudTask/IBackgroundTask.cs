using System;
using System.Linq.Expressions;

namespace GiveFoodInfrastructure.BackgroudTask
{
    public interface IBackgroundTask
    {
        string EnqueueOneTimeJob(Expression<Action> methodCall);
        string EnqueueOneTimeJob<T>(Expression<Action<T>> methodCall);
    }
}