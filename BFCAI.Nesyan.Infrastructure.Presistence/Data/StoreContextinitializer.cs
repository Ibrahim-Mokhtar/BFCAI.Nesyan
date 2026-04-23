using BFCAI.Nesyan.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using BFCAI.Nesyan.Domain.Entities.Primary.Doctors;
using BFCAI.Nesyan.Domain.Entities.Primary.Patients;
using BFCAI.Nesyan.Domain.Entities.Primary.Relatives;
using BFCAI.Nesyan.Domain.Entities.Primary.Caregivers;
using BFCAI.Nesyan.Domain.Entities.Alerts;
using BFCAI.Nesyan.Domain.Entities.MindGames;
using BFCAI.Nesyan.Domain.Entities.Reports;
using BFCAI.Nesyan.Domain.Entities.Medications;

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

        public async Task SeedAsync()
        {
            try
            {
                if (!DbContext.Doctors.Any())
                {
                    var doctorsData = await File.ReadAllTextAsync("../BFCAI.Nesyan.Infrastructure.Presistence/_Data/Seeds/doctors.json");
                    var doctors = JsonSerializer.Deserialize<List<Doctor>>(doctorsData);

                    if (doctors?.Count > 0)
                    {
                        await DbContext.Set<Doctor>().AddRangeAsync(doctors);
                        await DbContext.SaveChangesAsync();
                    }
                }

                if (!DbContext.Patients.Any())
                {
                    var patientsData = await File.ReadAllTextAsync("../BFCAI.Nesyan.Infrastructure.Presistence/_Data/Seeds/patients.json");
                    var patients = JsonSerializer.Deserialize<List<Patient>>(patientsData);

                    if (patients?.Count > 0)
                    {
                        await DbContext.Set<Patient>().AddRangeAsync(patients);
                        await DbContext.SaveChangesAsync();
                    }
                }

                if (!DbContext.Relatives.Any())
                {
                    var relativesData = await File.ReadAllTextAsync("../BFCAI.Nesyan.Infrastructure.Presistence/_Data/Seeds/relatives.json");
                    var relatives = JsonSerializer.Deserialize<List<Relative>>(relativesData);

                    if (relatives?.Count > 0)
                    {
                        await DbContext.Set<Relative>().AddRangeAsync(relatives);
                        await DbContext.SaveChangesAsync();
                    }
                }

                if (!DbContext.Caregivers.Any())
                {
                    var caregiversData = await File.ReadAllTextAsync("../BFCAI.Nesyan.Infrastructure.Presistence/_Data/Seeds/caregivers.json");
                    var caregivers = JsonSerializer.Deserialize<List<Caregiver>>(caregiversData);

                    if (caregivers?.Count > 0)
                    {
                        await DbContext.Set<Caregiver>().AddRangeAsync(caregivers);
                        await DbContext.SaveChangesAsync();
                    }
                }

                if (!DbContext.Alerts.Any())
                {
                    var alertsData = await File.ReadAllTextAsync("../BFCAI.Nesyan.Infrastructure.Presistence/_Data/Seeds/alerts.json");
                    var alerts = JsonSerializer.Deserialize<List<Alert>>(alertsData);

                    if (alerts?.Count > 0)
                    {
                        await DbContext.Set<Alert>().AddRangeAsync(alerts);
                        await DbContext.SaveChangesAsync();
                    }
                }

                if (!DbContext.MindGames.Any())
                {
                    var mindgamesData = await File.ReadAllTextAsync("../BFCAI.Nesyan.Infrastructure.Presistence/_Data/Seeds/mindgames.json");
                    var mindgames = JsonSerializer.Deserialize<List<MindGame>>(mindgamesData);

                    if (mindgames?.Count > 0)
                    {
                        await DbContext.Set<MindGame>().AddRangeAsync(mindgames);
                        await DbContext.SaveChangesAsync();
                    }
                }

                if (!DbContext.Medications.Any())
                {
                    var medicationsData = await File.ReadAllTextAsync("../BFCAI.Nesyan.Infrastructure.Presistence/_Data/Seeds/medications.json");
                    var medications = JsonSerializer.Deserialize<List<Medication>>(medicationsData);

                    if (medications?.Count > 0)
                    {
                        await DbContext.Set<Medication>().AddRangeAsync(medications);
                        await DbContext.SaveChangesAsync();
                    }
                }

                if (!DbContext.Reports.Any())
                {
                    var reportsData = await File.ReadAllTextAsync("../BFCAI.Nesyan.Infrastructure.Presistence/_Data/Seeds/reports.json");
                    var reports = JsonSerializer.Deserialize<List<Report>>(reportsData);

                    if (reports?.Count > 0)
                    {
                        await DbContext.Set<Report>().AddRangeAsync(reports);
                        await DbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during seeding: {ex.Message}");
            }
        }
    }
}
