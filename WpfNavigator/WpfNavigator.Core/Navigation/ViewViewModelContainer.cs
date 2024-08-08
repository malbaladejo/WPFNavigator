namespace WpfNavigator.Core.Navigation
{
    public class ViewViewModelContainer
    {
        public ViewViewModelContainer() : this(typeof(object), typeof(object))
        {

        }

        public ViewViewModelContainer(Type viewType, Type viewModelType)
        {
            this.ViewType = viewType;
            this.ViewModelType = viewModelType;
        }

        public Type ViewType { get; }
        public Type ViewModelType { get; }
    }
}
