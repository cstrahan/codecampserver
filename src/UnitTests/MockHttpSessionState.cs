using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Web.SessionState;

namespace CodeCampServer.UnitTests
{
    public class MockHttpSessionState : HttpSessionStateBase
    {
        private OrderedDictionary _store = new OrderedDictionary();

        public override void Abandon()
        {
            _store.Clear();
            _store = null;
            _store = new OrderedDictionary();
        }

        public override void Clear()
        {
            _store.Clear();
        }

        public override void Add(string name, object value)
        {
            _store.Add(name, value);
        }

        public override void CopyTo(Array array, int index)
        {
            _store.CopyTo(array, index);
        }

        public override HttpSessionStateBase Contents
        {
            get
            {
                return this;
            }
        }

        public override int Count
        {
            get
            {
                return _store.Count;
            }
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return _store.GetEnumerator();
        }

        public override void Remove(string name)
        {
            _store.Remove(name);
        }

        public override void RemoveAll()
        {
            _store.Clear();
        }

        public override void RemoveAt(int index)
        {
            _store.RemoveAt(index);
        }

        public override object this[int index]
        {
            get
            {
                return _store[index];
            }
            set
            {
                _store[index] = value;
            }
        }

        public override object this[string name]
        {
            get
            {
                return _store[name];
            }
            set
            {
                _store[name] = value;
            }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override string SessionID
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override HttpStaticObjectsCollection StaticObjects
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override int Timeout
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override HttpCookieMode CookieMode
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override bool IsCookieless
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override bool IsNewSession
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        //NameObjectCollectionBase.KeysCollection.ctor is marked internal
        public override NameObjectCollectionBase.KeysCollection Keys
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override int LCID
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override SessionStateMode Mode
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public override int CodePage
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
