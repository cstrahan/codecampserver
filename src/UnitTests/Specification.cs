using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests
{
    [TestFixture]
    public class Specification
    {
        private MockRepository _mocks;
        public MockRepository Mocks
        {
            get { return _mocks; }
        }

        public void BackToRecord(object mockObject)
        {
            Mocks.BackToRecord(mockObject);
        }

        public IDisposable Record
        {
            get { return _mocks.Record(); }
        }

        public IDisposable Playback
        {
            get { return _mocks.Playback(); }
        }
        
        public IDisposable PlaybackOnly
        {
            get
            {
                using (Record)
                {
                }
                return Playback;
            }
        }

        public T Mock<T>()
        {
            return Mocks.DynamicMock<T>();
        }

        public T StrictMock<T>()
        {
            return Mocks.CreateMock<T>();
        }

        public T Stub<T>()
        {
            return Mocks.Stub<T>();
        }

        [SetUp]
        public void Setup()
        {
            _mocks = new MockRepository();

            before_each();
        }

        public virtual void before_each()
        {
        }

        [TearDown]
        public void Teardown()
        {
            after_each();
        }

        public virtual void after_each()
        {
        }
    }
}
