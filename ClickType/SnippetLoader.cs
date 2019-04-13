using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
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

            var queryResults = dbConnection.Query<Snippet>("select * from Snippet");
            snippets = queryResults.ToList();

            return snippets;
        }

        public static void AddSnippet(string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                return;
            }

            string query = "insert into Snippet (SnippetText) values (@SnippetText);";
            var parameters = new DynamicParameters();
            parameters.Add("@SnippetText", text);

            dbConnection.Query<Snippet>(query, parameters);
        }

        public static void EditSnippet(long id, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            string query = "update Snippet set SnippetText = (@SnippetText) where Id = (@Id);";
            var parameters = new DynamicParameters();
            parameters.Add("@SnippetText", text);
            parameters.Add("@Id", id);

            dbConnection.Query<Snippet>(query, parameters);
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
