using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using dsl_play.common;
using dsl_play.models;

namespace dsl_play.language.Descriptors
{
    public static class ModelDescriptor
    {
        public static IModelDescriptor<TModel> Create<TModel>(string name) where TModel : IDataModel 
            => new ModelDescriptor<TModel>(name);
    }
    
    public class ModelDescriptor<TModel> : Descriptor, IModelDescriptor<TModel> 
        where TModel : IDataModel
    {
        private readonly List<IPropertyDescriptor<TModel>> _properties;
        
        protected internal ModelDescriptor(string name) : base(name) 
            => _properties = new List<IPropertyDescriptor<TModel>>();

        public Type Of => typeof(TModel);
        
        public IEnumerable<IPropertyDescriptor<TModel>> Properties 
            => _properties;

        public void AddProperty(IPropertyDescriptor<TModel> property)
            => AddPropertyAsync(property).Await();

        public Task AddPropertyAsync(IPropertyDescriptor<TModel> property, CancellationToken cancellationToken = default)
        {
            _properties.Add(property);
            return Task.CompletedTask;
        }
    }
}