using System;
using System.Collections.Concurrent;
using MakeNotes.Common.Interfaces;

namespace MakeNotes.Common.Core
{
    /// <summary>
    /// Represents a storage of global application variables. Supposed to be thread-safe.
    /// </summary>
    public class ApplicationState : IApplicationState
    {
        private readonly ConcurrentDictionary<string, Lazy<object>> _values;

        public ApplicationState()
        {
            _values = new ConcurrentDictionary<string, Lazy<object>>();
        }

        public void SetValue(string key, object value)
        {
            var lazy = new Lazy<object>(() => value);
            _values.AddOrUpdate(key, lazy, (k, v) => lazy);
        }

        public T GetValue<T>(string key)
        {
            var lazy = _values.GetOrAdd(key, k => new Lazy<object>(() => default(T)));
            return (T)lazy.Value;
        }
    }
}
