using System;
using System.Threading.Tasks;
using dsl_play.language.Descriptors;
using dsl_play.models;

namespace dsl_play.language.Actions;

public class SetPropertyAction<TModel>(string name, IPropertyDescriptor<TModel> descriptor) : 
    MutatePropertyAction<TModel>(name, descriptor) where TModel : class, IDataModel
{
    protected override Func<IPropertyDescriptor<TModel>, TModel, Task> Mutator => Mutate;

    private Task Mutate(IPropertyDescriptor<TModel> descriptor, TModel model)
    {
        var prop = descriptor.Type.GetProperty(descriptor.Name);
        if (prop == null) throw new ArgumentNullException("Descriptor Property not found!");
        
        return Task.CompletedTask;
    }
}