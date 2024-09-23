using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using dsl_play.common;
using dsl_play.language.Descriptors;
using dsl_play.models;

namespace dsl_play.language.Conditions
{
    public abstract class Condition : ICondition
    {
        public static ICondition<TModel> Create<TModel>(
            IPropertyDescriptor<TModel> property,
            Expression<Func<TModel, bool>> constraint)
            where TModel : class, IDataModel
            => new Condition<TModel>(property, constraint);
        
        
        public abstract bool IsMet(object model);
        public abstract Task<bool> IsMetAsync(object model, CancellationToken cancellationToken = default);
    }

    public class Condition<TModel> : ICondition<TModel> 
        where TModel : class, IDataModel
    {
        protected internal Condition(
            IPropertyDescriptor<TModel> property,
            Expression<Func<TModel, bool>> constraint)
        {
            Property = property;
            Constraint = constraint;
        }
        
        public IPropertyDescriptor<TModel> Property { get; }
        
        public Expression<Func<TModel, bool>> Constraint { get; }
        
        public bool IsMet(TModel model) => IsMetAsync(model).Await();

        public bool IsMet(object model) => IsMetAsync(model).Await();

        public Task<bool> IsMetAsync(object model, CancellationToken cancellationToken = default)
        {
            var getValue = Property.Of.Compile();
            var matchConstraint = Constraint.Compile();

            var convertedModel = model as TModel;
            var value = getValue(convertedModel);
            var isMet = matchConstraint(convertedModel);

            return Task.FromResult(isMet);
        }
        
        public Task<bool> IsMetAsync(TModel model, CancellationToken cancellationToken = default)
        {
            var getValue = Property.Of.Compile();
            var matchConstraint = Constraint.Compile();
            
            var value = getValue(model);
            var isMet = matchConstraint(model);

            return Task.FromResult(isMet);
        }
    }
}