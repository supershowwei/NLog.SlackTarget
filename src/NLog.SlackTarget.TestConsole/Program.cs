using System;
using NLog.Fluent;

namespace NLog.SlackTarget.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            var cmd = string.Empty;

            while ((cmd = Console.ReadLine()) != "q")
            {
                logger.Info("TEST: {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
                logger.Warn("TEST: {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);

                var msg = @"The network path was not found
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, UInt32 waitForMultipleObjectsTimeout, Boolean allowCreate, Boolean onlyOneCheckConnection, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionFactory.TryGetConnection(DbConnection owningConnection, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionInternal.TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionClosed.TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at System.Data.SqlClient.SqlConnection.TryOpenInner(TaskCompletionSource`1 retry)
   at System.Data.SqlClient.SqlConnection.TryOpen(TaskCompletionSource`1 retry)
   at System.Data.SqlClient.SqlConnection.Open()
   at WantGoo.QuoteAnalyzer.Repositories.QuoteAmplituteSqlRepository.Upsert()
   at WantGoo.QuoteAnalyzer.Repositories.SqlRepository`1.HandleSaveToDatabase()System.Data.SqlClient.SqlException (0x80131904): A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server) ---> System.ComponentModel.Win32Exception (0x80004005): The network path was not found
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, UInt32 waitForMultipleObjectsTimeout, Boolean allowCreate, Boolean onlyOneCheckConnection, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionFactory.TryGetConnection(DbConnection owningConnection, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionInternal.TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionClosed.TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at System.Data.SqlClient.SqlConnection.TryOpenInner(TaskCompletionSource`1 retry)
   at System.Data.SqlClient.SqlConnection.TryOpen(TaskCompletionSource`1 retry)
   at System.Data.SqlClient.SqlConnection.Open()
   at WantGoo.QuoteAnalyzer.Repositories.QuoteAmplituteSqlRepository.Upsert()
   at WantGoo.QuoteAnalyzer.Repositories.SqlRepository`1.HandleSaveToDatabase()
ClientConnectionId:00000000-0000-0000-0000-000000000000
Error Number:53,State:0,Class:20";

                logger.Error(msg);

                logger.Fatal("TEST: {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
            }
        }
    }
}