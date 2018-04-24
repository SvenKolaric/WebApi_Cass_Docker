using System;
using Cassandra;

namespace DAL
{
    public class CassandraDB
    {
        private Cluster _cluster;
        private ISession _session;

        public CassandraDB()
        {
            Connect();
        }

        private void Connect()
        {
            Builder cassandraBuilder = Cluster.Builder();
            //Connect to localDB for now
            cassandraBuilder.AddContactPoint("127.0.0.1");

            _cluster = cassandraBuilder.Build();
            _session = _cluster.Connect("webapicassdb");
        }

        protected ISession GetSession()
        {
            if (_session == null)
            {
                Connect();
            }

            return _session;
        }
    }
}
