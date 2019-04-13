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
        private static IDbConnection dbConnection;

        static SnippetLoader()
        {
            dbConnection = new SQLiteConnection(LoadConnectionString());
        }

        public static List<Snippet> LoadSnippets()
        {
            List<Snippet> snippets;

            var queryResults = dbConnection.Query<Snippet>("select * from Snippet", new DynamicParameters());
            snippets = queryResults.ToList();

            return snippets;
        }

        public static void AddSnippet(string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                return;
            }
            dbConnection.Query<Snippet>("insert into Snippet (SnippetText) values (\"" + text + "\");");
        }

        public static void EditSnippet(long id, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            dbConnection.Query<Snippet>("update Snippet set SnippetText = \'" + text + "\' where Id = " + id);
        }

        public static void DeleteSnippet(long snippetId)
        {
            dbConnection.Query<Snippet>("delete from Snippet where Id = " + snippetId);
        }

        private static string LoadConnectionString(string id="Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
