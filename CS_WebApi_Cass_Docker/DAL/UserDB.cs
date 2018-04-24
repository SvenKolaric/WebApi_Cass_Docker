using System;
using System.Collections.Generic;
using System.Text;
using Cassandra;


namespace DAL
{
    public class UserDB : CassandraDB
    {
        public RowSet GetUser(string _email)
        {
            string CQLstr = @"SELECT email, fullname, id, password, salt, role 
                              FROM webapicassdb.user_by_email 
                              WHERE email = ?";
            ISession localSession = GetSession();
            var statement1 = localSession.Prepare(CQLstr);

            RowSet result = localSession.Execute(statement1.Bind(_email));

            return result;

            //UserDB us = new UserDB();
            //var result = us.GetUser("admin.admin@gmail.com");

            //foreach (Row r in result)
            //{
            //    Console.WriteLine(r.GetValue<string>("email"));
            //    Console.ReadKey();
            //}
        }
    }
}
