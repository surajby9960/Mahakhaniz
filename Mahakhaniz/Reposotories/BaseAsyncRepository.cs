using System.Data.Common;
using System.Data.SqlClient;

namespace Mahakhaniz.Reposotories
{
    public class BaseAsyncRepository
    {
        private readonly string sqlReaderConnection;
        private readonly string Dbtype;
        public BaseAsyncRepository(IConfiguration configuration)
        {
            sqlReaderConnection = configuration.GetSection("DBInfo:sqlreaderConnectionString").Value;
            Dbtype = configuration.GetSection("DBInfo:dbType").Value;
        }
        internal DbConnection ReaderConnection
        {
            get
            {
                switch (Dbtype)
                {
                    case "SqlServer":
                        return new SqlConnection(sqlReaderConnection);
                    default:
                        return new SqlConnection(sqlReaderConnection);
                }
            }
        }
    }
}
