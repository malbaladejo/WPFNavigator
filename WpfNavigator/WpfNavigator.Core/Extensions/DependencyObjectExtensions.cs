using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using WpfNavigator.Core.Controls;
using WpfNavigator.Core.Windows;

namespace WpfNavigator.Core.Extensions
{
    internal static class DependencyObjectExtensions
    {
        public static IEnumerable<T> FindMyselfAndVisualChildren<T>([NotNull] this DependencyObject parent) where T : DependencyObject
        {
            if (parent is T castedParent)
                yield return castedParent;

            foreach (var item in parent.FindVisualChildren<T>())
                yield return item;
        }

        public static IEnumerable<T> FindVisualChildren<T>([NotNull] this DependencyObject parent) where T : DependencyObject
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            var queue = new Queue<DependencyObject>(new[] { parent });

            while (queue.Any())
            {
                var reference = queue.Dequeue();
                var count = VisualTreeHelper.GetChildrenCount(reference);

                for (var i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(reference, i);
                    if (child is T children)
                        yield return children;

                    queue.Enqueue(child);
                }
            }
        }

        public static IEnumerable<T> FindLogicalChildren<T>([NotNull] this DependencyObject parent) where T : DependencyObject
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            var queue = new Queue<DependencyObject>(new[] { parent });

            while (queue.Any())
            {
                var reference = queue.Dequeue();
                var children = LogicalTreeHelper.GetChildren(reference);
                var objects = children.OfType<DependencyObject>();

                foreach (var o in objects)
                {
                    if (o is T child)
                        yield return child;

                    queue.Enqueue(o);
                }
            }
        }

        public static INavigationWindow GetWindow(this DependencyObject item)
        {
            var window = Window.GetWindow(item) as INavigationWindow;

            if (window == null)
                throw new InvalidOperationException($"{nameof(NavigationControl)} must be used in {nameof(INavigationWindow)}");

            return window;
        }
    }
}
