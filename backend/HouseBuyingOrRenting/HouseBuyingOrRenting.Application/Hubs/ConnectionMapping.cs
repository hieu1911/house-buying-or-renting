using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HouseBuyingOrRenting.Application
{
    public class ConnectionMapping<T>
    {
        private readonly ConcurrentDictionary<T, HashSet<string>> _connections =
            new ConcurrentDictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            var connections = _connections.GetOrAdd(key, k => new HashSet<string>());
            lock (connections)
            {
                connections.Add(connectionId);
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections))
            {
                return;
            }

            lock (connections)
            {
                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    HashSet<string> ignored;
                    _connections.TryRemove(key, out ignored);
                }
            }
        }

        public void RemoveAll(T key)
        {
            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections))
            {
                return;
            }

            lock (connections)
            {
                connections.Clear();

                //if (connections.Count == 0)
                //{
                //    HashSet<string> ignored;
                //    _connections.TryRemove(key, out ignored);
                //}
            }
        }
    }

}
