namespace dsl_play.language.Descriptors
{
    public abstract class Descriptor : IDescriptor
    {
        protected Descriptor(string name)
            => DisplayName = name;
        
        public string DisplayName { get; }
    }
}