using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ClickType
{
    class SnippetLoader
    {
        public static List<Snippet> LoadSnippets()
        {
            List<Snippet> snippets;

            using (IDbConnection dbConnection = new SQLiteConnection(LoadConnectionString()))
            {
                var queryResults = dbConnection.Query<Snippet>("select * from Snippet", new DynamicParameters());
                snippets = queryResults.ToList();
            }

            return snippets;
        }

        private static string LoadConnectionString(string id="Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
