using System.Threading.Tasks;

namespace dsl_play.common
{
    public static class AsyncExtensions
    {
        public static void Await(this Task task)
        {
            if (task != null) task.Wait();
        }

        public static T Await<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }
    }
}