using System.ComponentModel;
using WpfNavigator.Core.Loggers;
using WpfNavigator.Core.Navigation;
using WpfNavigator.Core.ViewResolvers;

namespace WpfNavigator.Core.Windows
{
    public interface INavigatableWindow
    {
        Task NavigateAsync<TToken>(TToken token) where TToken : INavigationToken;

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

        IViewResolver ViewResolver { get; }

        ILogger Logger { get; }
    }
}
