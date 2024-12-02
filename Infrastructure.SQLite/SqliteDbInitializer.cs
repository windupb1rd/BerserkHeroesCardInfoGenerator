using Infrastructure.SQLite.Options;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace Infrastructure.SQLite
{
    public class SqliteDbInitializer
    {
        private readonly SqliteOptions _sqliteOptions;

        public SqliteDbInitializer(IOptions<SqliteOptions> sqliteOptions)
        {
            _sqliteOptions = sqliteOptions.Value;
        }

        public void Init()
        {
            var connection = new SqliteConnection(_sqliteOptions.SQLiteInMemoryConnectionString);
            connection.Open();
        }
    }
}
