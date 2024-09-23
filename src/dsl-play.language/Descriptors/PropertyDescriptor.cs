using System;
using System.Linq.Expressions;
using dsl_play.models;

namespace dsl_play.language.Descriptors
{
    public static class PropertyDescriptor
    {
        public static IPropertyDescriptor<TModel> Create<TModel>(
            Expression<Func<TModel, object>> expression) 
            where TModel : IDataModel 
            => new PropertyDescriptor<TModel>(expression);
        
        public static IPropertyDescriptor<TModel, TValue> Create<TModel, TValue>(
            Expression<Func<TModel, TValue>> expression) 
            where TModel : IDataModel 
            => new PropertyDescriptor<TModel, TValue>(expression);
    }
    
    public class PropertyDescriptor<TModel> : IPropertyDescriptor<TModel> where TModel : IDataModel
    {
        protected internal PropertyDescriptor(Expression<Func<TModel, object>> expression)
        {
            Of = expression;
        }

        public string DisplayName => GetName(Of);

        public string Name => GetName(Of);
        
        public Type Type => typeof(TModel);
        
        public Expression<Func<TModel, object>> Of { get; }

        protected string GetName(Expression<Func<TModel, object>> expression)
        {
            var existingProp = string.Empty;
            
            if (expression == null)
                return existingProp;

            if (!string.IsNullOrWhiteSpace(existingProp))
                return existingProp;

            var lambdaExpr = expression as LambdaExpression;

            var exprToCheck = lambdaExpr.Body;
            if (lambdaExpr.Body.NodeType == ExpressionType.Convert)
                exprToCheck = (lambdaExpr.Body as UnaryExpression)?.Operand;

            if (exprToCheck is not MemberExpression memberExpression) throw new InvalidOperationException(
                $"Expression is not a member access expression");

            existingProp = memberExpression.Member.Name;
            return existingProp;
        }
    }
    
    public class PropertyDescriptor<TModel, TValue> : 
        PropertyDescriptor<TModel>, 
        IPropertyDescriptor<TModel, TValue> 
        where TModel : IDataModel
    {
        protected internal PropertyDescriptor(Expression<Func<TModel, TValue>> expression) : 
            base(expression as Expression<Func<TModel, object>>) 
            => Of = expression;

        public new Expression<Func<TModel, TValue>> Of { get; }
    }
}