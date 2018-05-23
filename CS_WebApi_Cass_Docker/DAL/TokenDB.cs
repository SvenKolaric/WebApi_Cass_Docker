using System;
using System.Collections.Generic;
using System.Text;
using Cassandra;
using Cassandra.Mapping;

namespace DAL
{
    public class TokenDB : CassandraDB
    {
        public Entities.Token GetToken(string _email, string _devicename)
        {
            string CQLstr = @"SELECT tokendata, creationdate, email, expirationdate, devicename, blacklisted 
                              FROM webapicassdb.tokens 
                              WHERE email = ?
                              AND WHERE devicename = ?";

            ISession localSession = GetSession();
            IMapper mapper = new Mapper(localSession);
            //var statement1 = localSession.Prepare(CQLstr);

            //RowSet result = localSession.Execute(statement1.Bind(_email));

            var result = mapper.Single<Entities.Token>(CQLstr, _email, _devicename);

            return result;
        }

        public void SaveToken(Entities.Token _token)
        {
            ISession localSession = GetSession();

            var CQLstr = localSession.Prepare(@"INSERT INTO tokens (tokendata, creationdate, email, expirationdate, devicename, blacklisted) 
                                                VALUES (:tokendata, :crtDate, :email, :expDate, :device, :blacklisted)");

            //cassandra doesnt want to change timezone from gmt - hardcoded solution for now
            //_token.DeviceName = "desktop"; //mora biti definiran jer je sada primary
            localSession.Execute(CQLstr.Bind(new { tokendata = _token.TokenData, crtDate = _token.CreationDate.AddHours(2), email = _token.Email,
                                                   expDate = _token.ExpirationDate.AddHours(2), device = _token.DeviceName, blacklisted = false}));
        }
    }
}
