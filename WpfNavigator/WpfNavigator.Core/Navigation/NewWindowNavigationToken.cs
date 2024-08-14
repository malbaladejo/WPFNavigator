using System.Windows;

namespace WpfNavigator.Core.Navigation
{
    public class NewWindowNavigationToken : INavigationToken
    {
        public NewWindowNavigationToken(INavigationToken target)
        {
            this.Target = target;
        }

        public string Icon => this.Target.Icon;

        public string Label => this.Target.Label;

        public INavigationToken Target { get; }

        public double? Width { get; set; }

        public double? Height { get; set; }

        public WindowStartupLocation? WindowStartupLocation { get; set; }

        public SizeToContent? SizeToContent { get; set; }

        public bool CurrentWindowAsOwner { get; set; }

        /// <summary>
        /// Gets or sets the System.Windows.Window that owns this System.Windows.Window.
        /// </summary>
        /// <exception cref="ArgumentException">A window tries to own itself -or- Two windows try to own each other.</exception>
        /// <exception cref="InvalidOperationException">
        /// The System.Windows.Window.Owner property is set on a visible window shown using
        /// System.Windows.Window.ShowDialog -or- The System.Windows.Window.Owner property
        /// is set with a window that has not been previously shown.</exception>
        public Window? Owner { get; set; }
    }

}
