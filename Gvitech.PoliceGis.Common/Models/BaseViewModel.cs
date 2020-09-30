using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Mspace.Common.Models
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;

        public ICommand LoadedCommand
        {
            get { return _loadedCommand ?? (_loadedCommand = new RelayCommand(Loaded, () => true)); }
        }

        public ICommand UnloadedCommand
        {
            get { return _unloadedCommand ?? (_unloadedCommand = new RelayCommand(Unloaded, () => true)); }
        }


        protected virtual void Loaded()
        {

        }

        protected virtual void Unloaded()
        {
            Dispose();
        }
        public void Dispose()
        {

        }
        protected void OnPropertyChanged(Expression<Action> action)
        {
            string propertyName = GetPropertyName2(action);
            OnPropertyChanged(propertyName);
        }
        private static string GetPropertyName2(Expression<Action> action)
        {
            var expression = (MemberExpression)(action.Body);
            var propertyName = expression.Member.Name;
            return propertyName;
        }
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }
            return property.Name;
        }
    }
}
