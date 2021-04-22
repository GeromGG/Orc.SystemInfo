namespace Orc.SystemInfo.Win32
{
    using System.Collections;
    using System.Collections.Generic;
    using Catel;

    public sealed class WmiQuery : IEnumerable<WmiObject>
    {
        private readonly string _wql;
        private readonly WmiConnection _connection;

        private readonly EnumeratorBehaviorOption _enumeratorBehaviorOptions;

        public WmiQuery(WmiConnection connection, string wql, EnumeratorBehaviorOption enumeratorBehaviorOptions)
        {
            Argument.IsNotNull(() => connection);
            Argument.IsNotNull(() => wql);

            _wql = wql;
            _connection = connection;
            _enumeratorBehaviorOptions = enumeratorBehaviorOptions;
        }

        public WmiQuery(WmiConnection connection, string wql)
            : this(connection, wql, EnumeratorBehaviorOption.ReturnImmediately)
        {
        }

        public EnumeratorBehaviorOption EnumeratorBehaviorOption
        {
            get { return _enumeratorBehaviorOptions; }
        }

        public IEnumerator<WmiObject> GetEnumerator()
        {
            return _connection.ExecuteQuery(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
