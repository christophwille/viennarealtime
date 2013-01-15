using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MundlTransit.WP8.Common
{
    // http://stackoverflow.com/questions/5253622/databinding-textblock-runs-in-silverlight-wp7
    public static class BindableRuns
    {
        private static readonly Dictionary<INotifyPropertyChanged, PropertyChangedHandler>
            Handlers = new Dictionary<INotifyPropertyChanged, PropertyChangedHandler>();

        private static void TargetPropertyPropertyChanged(
                                        DependencyObject dependencyObject,
                                        DependencyPropertyChangedEventArgs e)
        {
            if (!(dependencyObject is TextBlock)) return;

            var textBlock = (TextBlock)dependencyObject;
            AddHandler(e.NewValue as INotifyPropertyChanged, textBlock);
            RemoveHandler(e.OldValue as INotifyPropertyChanged);
            InitializeRuns(textBlock, e.NewValue);
        }

        private static void AddHandler(INotifyPropertyChanged dataContext,
                                       TextBlock textBlock)
        {
            if (dataContext == null) return;

            var propertyChangedHandler = new PropertyChangedHandler(textBlock);
            dataContext.PropertyChanged += propertyChangedHandler.PropertyChanged;
            Handlers[dataContext] = propertyChangedHandler;
        }

        private static void RemoveHandler(INotifyPropertyChanged dataContext)
        {
            if (dataContext == null || !Handlers.ContainsKey(dataContext)) return;

            dataContext.PropertyChanged -= Handlers[dataContext].PropertyChanged;
            Handlers.Remove(dataContext);
        }

        private static void InitializeRuns(TextBlock textBlock, object dataContext)
        {
            if (dataContext == null) return;

            var runs = from run in textBlock.Inlines.OfType<Run>()
                       let propertyName = (string)run.GetValue(TargetProperty)
                       where propertyName != null
                       select new { Run = run, PropertyName = propertyName };


            foreach (var run in runs)
            {
                var property = dataContext.GetType().GetProperty(run.PropertyName);
                run.Run.Text = (string)property.GetValue(dataContext, null);
            }
        }

        private class PropertyChangedHandler
        {
            private readonly TextBlock _textBlock;
            public PropertyChangedHandler(TextBlock textBlock)
            {
                _textBlock = textBlock;
            }

            public void PropertyChanged(object sender,
                                        PropertyChangedEventArgs propertyChangedArgs)
            {
                var propertyName = propertyChangedArgs.PropertyName;
                var run = _textBlock.Inlines.OfType<Run>()
                    .Where(r => (string)r.GetValue(TargetProperty) == propertyName)
                    .SingleOrDefault();
                if (run == null) return;

                var property = sender.GetType().GetProperty(propertyName);
                run.Text = (string)property.GetValue(sender, null);
            }

        }


        public static object GetTarget(DependencyObject obj)
        {
            return obj.GetValue(TargetProperty);
        }

        public static void SetTarget(DependencyObject obj,
            object value)
        {
            obj.SetValue(TargetProperty, value);
        }

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.RegisterAttached("Target",
                typeof(object),
                typeof(BindableRuns),
                new PropertyMetadata(null,
                    TargetPropertyPropertyChanged));

    }
}
