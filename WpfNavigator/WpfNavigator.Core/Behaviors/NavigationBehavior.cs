using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;
using WpfNavigator.Core.Extensions;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.Windows;

namespace WpfNavigator.Core.Behaviors
{
    public class NavigationBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += this.OnMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonDown -= this.OnMouseLeftButtonDown;
        }

        public INavigationToken NavigationToken
        {
            get { return (INavigationToken)GetValue(NavigationTokenProperty); }
            set { SetValue(NavigationTokenProperty, value); }
        }

        public static readonly DependencyProperty NavigationTokenProperty =
            DependencyProperty.Register(nameof(NavigationToken), typeof(INavigationToken), typeof(NavigationBehavior));

        private async void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.NavigationToken == null)
                return;

            var token = this.NavigationToken;

            if (Keyboard.Modifiers == ModifierKeys.Control)
                token = token.OpenInNewWindow();

            await this.NavigateAsync(token);
        }

        private async Task NavigateAsync(INavigationToken token)
        {
            if (this.CurrentWindow == null)
                return;

            await this.CurrentWindow.NavigationService.NavigateAsync(token);
        }

        private INavigationWindow CurrentWindow => this.AssociatedObject.GetWindow();
    }
}
