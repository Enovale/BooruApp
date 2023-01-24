using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace BooruApp.Api.Helpers
{
    public class ObservableChangesCollection<T> :
        ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public ObservableChangesCollection()
        {
        }

        public ObservableChangesCollection(IEnumerable<T> collection) : this(collection.ToList())
        {
        }

        public ObservableChangesCollection(List<T> list) : base(list)
        {
            RegisterPropertyChanged(list);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                RegisterPropertyChanged(e.NewItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                UnRegisterPropertyChanged(e.OldItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                UnRegisterPropertyChanged(e.OldItems);
                RegisterPropertyChanged(e.NewItems);
            }

            base.OnCollectionChanged(e);
        }

        protected override void ClearItems()
        {
            UnRegisterPropertyChanged(this);
            base.ClearItems();
        }

        private void RegisterPropertyChanged(IEnumerable items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                if (item != null)
                {
                    item.PropertyChanged += ItemPropertyChanged;
                }
            }
        }

        private void UnRegisterPropertyChanged(IEnumerable items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                if (item != null)
                {
                    item.PropertyChanged -= ItemPropertyChanged;
                }
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}