using System.ComponentModel;
using System.Windows;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.ViewResolvers;

namespace WpfNavigator.Core.Windows
{
    public interface INavigationWindow
    {
        internal Task NavigateAsync<TToken>(TToken token) where TToken : INavigationToken;

        ///<summary>
        /// Opens a window and returns without waiting for the newly opened window to close.
        ///</summary>
        ///<exception cref="InvalidOperationException">
        /// System.Windows.Window.Show is called on a window that is closing (System.Windows.Window.Closing)
        /// or has been closed (System.Windows.Window.Closed).
        /// </exception>
        void Show();

        /// <summary>
        /// Occurs directly after System.Windows.Window.Close is called, and can be handled
        /// to cancel window closure.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// System.Windows.UIElement.Visibility is set, or System.Windows.Window.Show, System.Windows.Window.ShowDialog,
        /// or System.Windows.Window.Close is called while a window is closing.
        /// </exception>
        event CancelEventHandler Closing;

        /// <summary>
        /// Occurs when the window is about to close.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// System.Windows.UIElement.Visibility is set, or System.Windows.Window.Show, System.Windows.Window.ShowDialog,
        /// or System.Windows.Window.Hide is called while a window is closing.
        /// </exception>
        event EventHandler Closed;

        //
        // Summary:
        //     Gets or sets a value that indicates whether a window will automatically size
        //     itself to fit the size of its content.
        //
        // Returns:
        //     A System.Windows.SizeToContent value. The default is System.Windows.SizeToContent.Manual.
        public SizeToContent SizeToContent { get; set; }

        //
        // Summary:
        //     Gets or sets the width of the element.
        //
        // Returns:
        //     The width of the element, in device-independent units (1/96th inch per unit).
        //     The default value is System.Double.NaN. This value must be equal to or greater
        //     than 0.0. See Remarks for upper bound information.
        public double Width { get; set; }

        //
        // Summary:
        //     Gets or sets the suggested height of the element.
        //
        // Returns:
        //     The height of the element, in device-independent units (1/96th inch per unit).
        //     The default value is System.Double.NaN. This value must be equal to or greater
        //     than 0.0.
        public double Height { get; set; }

        //
        // Summary:
        //     Gets or sets the System.Windows.Window that owns this System.Windows.Window.
        //
        //
        // Returns:
        //     A System.Windows.Window object that represents the owner of this System.Windows.Window.
        //
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     A window tries to own itself -or- Two windows try to own each other.
        //
        //   T:System.InvalidOperationException:
        //     The System.Windows.Window.Owner property is set on a visible window shown using
        //     System.Windows.Window.ShowDialog -or- The System.Windows.Window.Owner property
        //     is set with a window that has not been previously shown.
        public Window Owner { get; set; }

        //
        // Summary:
        //     Gets or sets the position of the window when first shown.
        //
        // Returns:
        //     A System.Windows.WindowStartupLocation value that specifies the top/left position
        //     of a window when first shown. The default is System.Windows.WindowStartupLocation.Manual.
        public WindowStartupLocation WindowStartupLocation { get; set; }

        IViewResolver ViewResolver { get; }

        INavigationService NavigationService { get; set; }

        ILogger Logger { get; }

        Containers.IContainer Container { get; }
    }
}
