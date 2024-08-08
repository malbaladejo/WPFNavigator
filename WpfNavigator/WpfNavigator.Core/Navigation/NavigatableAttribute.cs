namespace WpfNavigator.Core.Navigation
{
    public class NavigatableAttribute : Attribute
    {
        public NavigatableAttribute(Type viewType, Type viewModelType)
        {
            this.ViewType = viewType;
            this.ViewModelType = viewModelType;
        }

        public Type ViewType { get; }
        public Type ViewModelType { get; }
    }
}
