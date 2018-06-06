using System;
using System.Collections.Generic;
using System.Text;
using Cassandra;
using Cassandra.Mapping;


namespace DAL
{
    public class UserDB : CassandraDB
    {
        public Entities.User GetUser(string _email)
        {
            string CQLstr = @"SELECT email, fullname, password, salt, role 
                              FROM webapicassdb.user_by_email 
                              WHERE email = ?";

            ISession localSession = GetSession();
            IMapper mapper = new Mapper(localSession);

            var result = mapper.Single<Entities.User>(CQLstr, _email);

            return result;
        }

        public void SaveUser(Entities.User _user)
        {
            ISession localSession = GetSession();

            var CQLstr = localSession.Prepare(@"INSERT INTO user_by_email (email, fullname, password, salt, role) 
                                              VALUES (:email, :name, :password, :salt, :role)");

            localSession.Execute(CQLstr.Bind(new
            {
                email = _user.Email,
                name = _user.FullName,
                password = _user.Password,
                salt = _user.Salt,
                role = "user"
            }));
        }
    }
}

