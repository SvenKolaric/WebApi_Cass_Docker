using System;
using System.Collections.Generic;
using System.Text;
using Cassandra;
using Cassandra.Mapping;

namespace DAL
{
    public class TokenDB : CassandraDB
    {
        public Entities.Token GetToken(string _email)
        {
            string CQLstr = @"SELECT tokendata, creationdate, email, expirationdate 
                              FROM webapicassdb.tokens 
                              WHERE email = ?";

            ISession localSession = GetSession();
            IMapper mapper = new Mapper(localSession);
            //var statement1 = localSession.Prepare(CQLstr);

            //RowSet result = localSession.Execute(statement1.Bind(_email));

            var result = mapper.Single<Entities.Token>(CQLstr, _email);

            return result;
        }

        public void SaveToken(Entities.Token _token)
        {
            ISession localSession = GetSession();

            var CQLstr = localSession.Prepare(@"INSERT INTO tokens (tokendata, creationdate, email, expirationdate) 
                                                VALUES (:tokendata, :crtDate, :email, :expDate)");

            localSession.Execute(CQLstr.Bind(new { tokendata = _token.TokenData, crtDate = _token.CreationDate, email = _token.Email, expDate = _token.ExpirationDate }));
        }
    }
}
