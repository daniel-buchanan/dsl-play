using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using dsl_play.common;
using dsl_play.language.Actions;
using dsl_play.language.Conditions;
using dsl_play.models;

namespace dsl_play.language
{
    public class Statement(RootNode conditions, IEnumerable<ModelAction> actions) : IStatement
    {
        public static Statement With(RootNode conditions, IEnumerable<ModelAction> actions)
            => new Statement(conditions, actions);

        public static Statement<TModel> With<TModel>(RootNode conditions, IEnumerable<ModelAction<TModel>> actions) 
            where TModel : class, IDataModel 
            => new Statement<TModel>(conditions, actions);

        public RootNode Conditions { get; } = conditions;
        public IEnumerable<ModelAction> Actions { get; } = actions;

        public object Apply(object model)
            => ApplyAsync(model).Await();

        public async Task<object> ApplyAsync(object model, CancellationToken cancellationToken = default)
        {
            if (!Conditions.AllMet(model))
                throw new ConditionsNotMetException();

            foreach (var a in Actions)
                await a.ExecuteAsync([model], cancellationToken);

            return model;
        }
    }

    public class Statement<TModel>(RootNode conditions, IEnumerable<ModelAction<TModel>> actions) : 
        Statement(conditions, actions), IStatement<TModel> 
        where TModel : class, IDataModel
    {
        public new IEnumerable<ModelAction<TModel>> Actions { get; } = actions.ToList();
        
        public TModel Apply(TModel model)
            => ApplyAsync(model).Await();

        public async Task<TModel> ApplyAsync(TModel model, CancellationToken cancellationToken = default)
        {
            if (!Conditions.AllMet(model))
                throw new ConditionsNotMetException();
            await base.ApplyAsync(model, cancellationToken);

            return model;
        }
    }
}