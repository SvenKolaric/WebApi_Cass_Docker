using System;
using Cassandra;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            Builder cassandraBuilder = Cluster.Builder();
            cassandraBuilder.AddContactPoint("127.0.0.1");

            var cluster = cassandraBuilder.Build();

            ISession session = cluster.Connect("webapicassdb");

            RowSet rowSet = session.Execute("select * from users");


            //foreach (var row in rowSet)
            //{
            //    foreach (var roww in row)
            //        Console.Write(roww.ToString() + ' ');
            //    Console.WriteLine();
            //}

            //Console.Read();
        }
    }
}
