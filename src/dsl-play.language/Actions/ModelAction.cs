using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using dsl_play.common;
using dsl_play.models;

namespace dsl_play.language.Actions
{
    public abstract class ModelAction(string name) : IModelAction
    {
        public string Name { get; } = name;

        protected abstract Func<object, Task> Action { get; }
        
        public void Execute(params object[] args)
            => ExecuteAsync(args).Await();

        public virtual async Task ExecuteAsync(object[] args, CancellationToken cancellationToken = default)
        {
            if (args == null || !args.Any()) return;

            foreach (var m in args)
                await Action(m);
        }
    }

    public abstract class ModelAction<TModel> : ModelAction, IModelAction<TModel> 
        where TModel : IDataModel
    {
        protected ModelAction(string name) : base(name) { }
    }
}