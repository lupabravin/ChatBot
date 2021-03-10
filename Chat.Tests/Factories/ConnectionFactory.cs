using Chat.Infrastructure.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Tests.Factories
{
    public class ConnectionFactory : IDisposable
    {

        #region IDisposable Support

        public ChatContext CreateTestContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<ChatContext>().UseSqlite(connection).Options;

            var context = new ChatContext(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public void Dispose()
        {
            Dispose();
        }



        #endregion
    }
}
