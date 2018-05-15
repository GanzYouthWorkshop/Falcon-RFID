using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.Falcon.RFID.Client
{
    [Serializable]
    public class KeyedCollection<K, T> : List<T>
    {
        private Func<T, K> m_ItemKeyPredicate;

        public KeyedCollection(Func<T, K> itemKey)
        {
            this.m_ItemKeyPredicate = itemKey;
        }

        public T this[K o]
        {
            get
            {
                return this.FirstOrDefault(item => this.m_ItemKeyPredicate(item).Equals(o));
            }

            set
            {
                try
                {
                    T item = this[o];

                    //Ha item nincs benne a listában exception-t dob!
                    this.Remove(item);
                }
                finally
                {
                    this.Add(value);
                }
            }
        }

    }

    [Serializable]
    public class KeyedCollection<T> : KeyedCollection<object, T>
    {
        public KeyedCollection(Func<T, IComparable> itemKey) : base(itemKey)
        {
        }
    }
}
