using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfNavigator.Core.Extensions;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.Windows;

namespace WpfNavigator.Core.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:navigationControls="clr-namespace:WpfNavigator.Core.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:navigationControls="clr-namespace:WpfNavigator.Core.Controls;assembly=WpfNavigator.Core"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <navigationControls:NavigationLink/>
    ///
    /// </summary>
    public class NavigationLink : Control
    {
        static NavigationLink()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationLink), new FrameworkPropertyMetadata(typeof(NavigationLink)));
        }

        public NavigationLink()
        {
            this.MouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        private async void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.NavigationToken == null)
                return;

            var token = this.NavigationToken;

            if (Keyboard.Modifiers == ModifierKeys.Control)
                token = token.OpenInNewWindow();

            await this.NavigateAsync(token);
        }

        public INavigationToken NavigationToken
        {
            get { return (INavigationToken)GetValue(NavigationTokenProperty); }
            set { SetValue(NavigationTokenProperty, value); }
        }

        public static readonly DependencyProperty NavigationTokenProperty =
            DependencyProperty.Register(nameof(NavigationToken), typeof(INavigationToken), typeof(NavigationLink));

        private async Task NavigateAsync(INavigationToken token)
        {
            if (this.CurrentWindow == null)
                return;

            await this.CurrentWindow.NavigationService.NavigateAsync(token);
        }

        private INavigationWindow CurrentWindow => DependencyObjectExtensions.GetWindow(this);
    }
}
