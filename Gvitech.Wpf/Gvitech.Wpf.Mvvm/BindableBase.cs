using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mmc.Wpf.Mvvm
{
	public class BindableBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
        protected void SetAndNotifyPropertyChanged<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
		{
			if (!EqualityComparer<T>.Default.Equals(field, newValue))
			{
				field = newValue;
				this.NotifyPropertyChanged(propertyName);
			}
		}
	}
}
