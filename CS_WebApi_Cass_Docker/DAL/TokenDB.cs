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
                              AND devicename = ?";

            ISession localSession = GetSession();
            IMapper mapper = new Mapper(localSession);
            //var statement1 = localSession.Prepare(CQLstr);

            //RowSet result = localSession.Execute(statement1.Bind(_email));

            var result = mapper.Single<Entities.Token>(CQLstr, _email, _devicename);

            return result;
        }

        public IEnumerable<Entities.Token> GetTokensByUser(string _email)
        {
            ISession localSession = GetSession();
            IMapper mapper = new Mapper(localSession);

            var CQLstr = localSession.Prepare(@"SELECT tokendata, creationdate, email, expirationdate, devicename, blacklisted
                              FROM tokens 
                              WHERE email = ?");

            //MappingConfiguration.Global.Define(
            //new Map<Entities.Token>()
            //  .TableName("tokens")
            //  .PartitionKey(u => u.Email)
            //  .ClusteringKey(u => u.DeviceName)
            //  .Column(u => u.Email, cm => cm.WithName("email"))
            //  .Column(u => u.DeviceName, cm => cm.WithName("devicename"))
            //  .Column(u => u.Blacklisted, cm => cm.WithName("blacklisted"))
            //  .Column(u => u.CreationDate, cm => cm.WithName("creationdate"))
            //  .Column(u => u.ExpirationDate, cm => cm.WithName("expirationdate"))
            //  .Column(u => u.TokenData, cm => cm.WithName("tokendata")));


            //IEnumerable<Entities.Token> result = mapper.Fetch<Entities.Token>(CQLstr, _email);

            List<Entities.Token> result = new List<Entities.Token>();

            var rs = localSession.Execute(CQLstr.Bind(new { email = _email }));
            foreach (var row in rs)
            {
                Entities.Token t = new Entities.Token()
                {
                    Email = _email,
                    DeviceName = row.GetValue<string>("devicename"),
                    CreationDate = row.GetValue<DateTime>("creationdate"),
                    ExpirationDate = row.GetValue<DateTime>("expirationdate"),
                    Blacklisted =row.GetValue<Boolean>("blacklisted"),
                    TokenData = row.GetValue<string>("tokendata")
                };
                result.Add(t);
            }

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

        public void DeleteToken(string _email, string _deviceName)
        {
            ISession localSession = GetSession();

            var CQLstr = localSession.Prepare("DELETE FROM tokens WHERE email = :email and devicename = :devicename");

            localSession.Execute(CQLstr.Bind(new { email = _email, devicename = _deviceName }));
        }
    }
}
