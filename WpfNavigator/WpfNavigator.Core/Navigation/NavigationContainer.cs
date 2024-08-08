using System.Reflection;
using System.Windows;

namespace WpfNavigator.Core.Navigation
{
    public class NavigationContainer : INavigationContainer
    {
        private readonly IDictionary<Type, ViewViewModelContainer> container = new Dictionary<Type, ViewViewModelContainer>();

        public void Register<TToken, TView, TViewModel>()
            where TToken : INavigationToken
            where TView : FrameworkElement
        {
            this.container[typeof(TToken)] = new ViewViewModelContainer(typeof(TView), typeof(TViewModel));
        }

        public bool IsRegistered(INavigationToken token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            return this.container.ContainsKey(token.GetType());
        }

        public ViewViewModelContainer Resolve(INavigationToken token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (this.TryResolveByAttribute(token, out ViewViewModelContainer viewViewModelContainer1))
                return viewViewModelContainer1;

            if (this.TryResolveByRegister(token, out ViewViewModelContainer viewViewModelContainer2))
                return viewViewModelContainer2;

            var errorMessage = $"There is no View/ViewModel register for {token.GetType()}.{Environment.NewLine}";
            errorMessage += $"You have 2 options to register View/ViewModel for this token:{Environment.NewLine}";
            errorMessage += $"1. Decores the token with {nameof(NavigatableAttribute)}.{Environment.NewLine}";
            errorMessage += $"2. Uses method {nameof(INavigationContainer)}.{nameof(Register)}<TToken, TView, TViewModel>";
            throw new ArgumentOutOfRangeException(errorMessage);
        }

        private bool TryResolveByAttribute(INavigationToken token, out ViewViewModelContainer viewViewModelContainer)
        {
            viewViewModelContainer = new ViewViewModelContainer();
            var attribute = token.GetType().GetCustomAttribute<NavigatableAttribute>();
            if (attribute == null)
                return false;

            viewViewModelContainer = new ViewViewModelContainer(attribute.ViewType, attribute.ViewModelType);
            return true;
        }
        private bool TryResolveByRegister(INavigationToken token, out ViewViewModelContainer viewViewModelContainer)
        {
            viewViewModelContainer = new ViewViewModelContainer(typeof(object), typeof(object));
            if (!this.container.ContainsKey(token.GetType()))
                return false;

            viewViewModelContainer = this.container[token.GetType()];
            return true;
        }
    }
}
