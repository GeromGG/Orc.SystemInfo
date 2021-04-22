namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using Catel;
    using Orc.SystemInfo.Win32;

    public class WmiObject : IDisposable
    {
        private const string GenusPropertyName = "__genus";
        private const string ClassPropertyName = "__class";
        private const string SuperClassPropertyName = "__superclass";
        private const string DynastyPropertyName = "__dynasty";
        private const string RelpathPropertyName = "__relpath";
        private const string PropertyCountPropertyName = "__property_count";
        private const string DerivationPropertyName = "__derivation";
        private const string ServerPropertyName = "__server";
        private const string NamespacePropertyName = "__namespace";
        private const string PathPropertyName = "__path";

        private readonly IWbemClassObject _wbemClassObject;

        private bool _disposed;

        internal WmiObject(IWbemClassObject wmiObject)
        {
             Argument.IsNotNull(() => wmiObject);

            _wbemClassObject = wmiObject;
        }

        public WmiObjectGenus Genus
        {
            get
            {
                return (WmiObjectGenus)GetPropertyValue(WmiObject.GenusPropertyName);
            }
        }

        public string Class
        {
            get
            {
                return (string)GetPropertyValue(WmiObject.ClassPropertyName);
            }
        }

        public string SuperClass
        {
            get
            {
                return (string)GetPropertyValue(WmiObject.SuperClassPropertyName);
            }
        }

        public string Dynasty
        {
            get
            {
                return (string)GetPropertyValue(WmiObject.DynastyPropertyName);
            }
        }

        public string Relpath
        {
            get
            {
                return (string)GetPropertyValue(WmiObject.RelpathPropertyName);
            }
        }

        public int PropertyCount
        {
            get
            {
                return (int)GetPropertyValue(WmiObject.PropertyCountPropertyName);
            }
        }

        public string[] Derivation
        {
            get
            {
                return (string[])GetPropertyValue(WmiObject.DerivationPropertyName);
            }
        }

        public string Server
        {
            get
            {
                return (string)GetPropertyValue(WmiObject.ServerPropertyName);
            }
        }

        public string Namespace
        {
            get
            {
                return (string)GetPropertyValue(WmiObject.NamespacePropertyName);
            }
        }

        public string Path
        {
            get
            {
                return (string)GetPropertyValue(WmiObject.PathPropertyName);
            }
        }

        public object this[string propertyName]
        {
            get
            {
                return GetPropertyValue(propertyName);
            }
        }

        public IEnumerable<string> GetPropertyNames()
        {
            return _wbemClassObject.GetNames();
        }

        public object GetPropertyValue(string propertyName)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(typeof(WmiObject).FullName);
            }

            return _wbemClassObject.Get(propertyName);
        }

        public TResult GetPropertyValue<TResult>(string propertyName)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(typeof(WmiObject).FullName);
            }

            return (TResult)_wbemClassObject.Get(propertyName);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Marshal.ReleaseComObject(_wbemClassObject);
                _disposed = true;
            }
        }

        public override string ToString()
        {
            return Class;
        }
    }
}
