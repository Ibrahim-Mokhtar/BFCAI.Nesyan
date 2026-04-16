using BFCAI.Nesyan.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Infrastructure.Presistence.Data
{
    public class StoreContextinitializer(StoreContext DbContext) : IStoreContextInitializer
    {
        public async Task InitalizeAsync()
        {
            // If the DB already contains tables but __EFMigrationsHistory is missing/empty,
            // calling Migrate() will try to re-create tables (and spam errors like "Alerts already exists").
            // We detect that state and skip migration to keep startup clean.

            var db = DbContext.Database;
            var hasHistory = false;
            var hasExistingTables = false;
            var conn = db.GetDbConnection();
            await conn.OpenAsync();
            try
            {
                bool TableExists(string tableName)
                {
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT OBJECT_ID(@p0);";
                    var p = cmd.CreateParameter();
                    p.ParameterName = "@p0";
                    p.DbType = DbType.String;
                    p.Value = $"[dbo].[{tableName}]";
                    cmd.Parameters.Add(p);
                    var result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value;
                }

                bool HasMigrationHistoryRows()
                {
                    if (!TableExists("__EFMigrationsHistory"))
                        return false;

                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT TOP(1) 1 FROM [__EFMigrationsHistory];";
                    var result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value;
                }

                hasHistory = HasMigrationHistoryRows();
                hasExistingTables = TableExists("Alerts"); // any known table from your schema

                if (!hasHistory && hasExistingTables)
                {
                    Console.WriteLine("Database migration skipped: existing schema detected but migrations history is missing/out-of-sync.");
                    return;
                }
            }
            finally
            {
                await conn.CloseAsync();
            }

            var pendingMigrations = await DbContext.Database.GetPendingMigrationsAsync();
            if (!pendingMigrations.Any())
            {
                Console.WriteLine("No pending migrations.");
                return;
            }

            // If the DB already has tables, but InitialCreate is still pending, EF will try to create existing objects.
            // This indicates migrations history is out-of-sync with the actual schema. Skip to keep startup clean.
            if (hasExistingTables && pendingMigrations.Contains("20260416134014_InitialCreate"))
            {
                Console.WriteLine("Database migration skipped: InitialCreate is pending but the schema already exists (migrations history out-of-sync).");
                return;
            }

            await DbContext.Database.MigrateAsync();
            Console.WriteLine("Database migrated successfully.");
        }


    }
}
