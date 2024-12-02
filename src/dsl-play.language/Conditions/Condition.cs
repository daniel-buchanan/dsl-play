using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using dsl_play.common;
using dsl_play.language.Descriptors;
using dsl_play.language.Json.Converters;
using dsl_play.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dsl_play.language.Conditions
{
    public class Condition : ICondition
    {
        public static Condition<TModel> Create<TModel>(
            IPropertyDescriptor<TModel> property,
            Expression<Func<TModel, bool>> constraint)
            where TModel : class, IDataModel
            => new Condition<TModel>(property, constraint);
        
        
        public virtual bool IsMet(object model) => IsMetAsync(model).Await();
        public virtual Task<bool> IsMetAsync(object model, CancellationToken cancellationToken = default) => Task.FromResult(false);
        
        public PropertyDescriptor Property { get; }
    }

    public class Condition<TModel> : Condition, ICondition<TModel> 
        where TModel : class, IDataModel
    {
        [JsonConstructor]
        private Condition(PropertyDescriptor<TModel> property, Expression<Func<TModel, bool>> constraint)
        {
            Property = property;
            Constraint = constraint;
        }
        
        protected internal Condition(
            IPropertyDescriptor<TModel> property,
            Expression<Func<TModel, bool>> constraint)
        {
            Property = property;
            Constraint = constraint;
        }
        
        public new IPropertyDescriptor<TModel> Property { get; }
        
        [JsonConverter(typeof(ExpressionConverter))]
        public Expression<Func<TModel, bool>> Constraint { get; }
        
        public bool IsMet(TModel model) => IsMetAsync(model).Await();

        public new Task<bool> IsMetAsync(object model, CancellationToken cancellationToken = default)
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