using HPASS.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace HPASS.EfCore.Context
{
    public class HpassDbContext : DbContext
    {

        private readonly IConfiguration appConfig;

        private string databaseServiceAdress;
        private int databaseServicePort;
        private bool databaseSslMode;
        private string databaseName;
        private string databaseUserName;
        private string databasePassword;
        private string applicationName;

        public HpassDbContext(IConfiguration appConfig)
        {
            this.appConfig = appConfig;
            this.ObtainAndCheckDbAccessParameters();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseNpgsql(this.GeneratePostgreSqlConnection());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Patient> patientFluentModel = modelBuilder.Entity<Patient>();
            patientFluentModel.ToTable("Patients");
            patientFluentModel.HasKey(u => u.Id);
            patientFluentModel
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId);
            patientFluentModel
                .HasMany(x => x.MedicalHistories)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId);


            EntityTypeBuilder<HealthcareProvider> healthcareProviderFluentModel = modelBuilder.Entity<HealthcareProvider>();
            healthcareProviderFluentModel.ToTable("HealthcareProviders");
            healthcareProviderFluentModel.HasKey(x => x.Id);
            healthcareProviderFluentModel
                .HasMany(x => x.OperationZones)
                .WithOne(x => x.HealthcareProvider)
                .HasForeignKey(x => x.HealthcareProviderId)
                .IsRequired(true);
            healthcareProviderFluentModel
                .HasMany(x => x.Users)
                .WithOne(x => x.HealthcareProvider)
                .HasForeignKey(x => x.HealthcareProviderId)
                .IsRequired(true);
            healthcareProviderFluentModel
               .HasMany(x => x.Doctors)
               .WithOne(x => x.HealthcareProvider)
               .HasForeignKey(x => x.HealthcareProviderId)
               .IsRequired(true);
            healthcareProviderFluentModel
               .HasMany(x => x.Appointments)
               .WithOne(x => x.HealthcareProvider)
               .HasForeignKey(x => x.HealthcareProviderId)
               .IsRequired(true);

            EntityTypeBuilder<OperationZone> operationZoneFluentModel = modelBuilder.Entity<OperationZone>();
            operationZoneFluentModel.ToTable("OperationZones");
            operationZoneFluentModel.HasKey(x => x.Id);
            operationZoneFluentModel
                .HasMany(x => x.Appointments)
                .WithOne(x => x.OperationZone)
                .HasForeignKey(x => x.OperationZoneId);

            EntityTypeBuilder<Doctor> doctorFluentModel = modelBuilder.Entity<Doctor>();
            doctorFluentModel.ToTable("Doctors");
            doctorFluentModel.HasKey(x => x.Id);
            doctorFluentModel
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Doctor)
                .HasForeignKey(x => x.DoctorId);

            EntityTypeBuilder<Department> departmentFluentModel = modelBuilder.Entity<Department>();
            departmentFluentModel.ToTable("Departments");
            departmentFluentModel.HasKey(x => x.Id);
            departmentFluentModel
                .HasMany(x => x.Doctors)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId);
            departmentFluentModel
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId);

            EntityTypeBuilder<Appointment> appointmentFluentModel = modelBuilder.Entity<Appointment>();
            appointmentFluentModel.ToTable("Appointments");
            appointmentFluentModel.HasKey(x => x.Id);
            appointmentFluentModel
                .HasMany(x => x.AppointmentReminderLogs)
                .WithOne(x => x.Appointment)
                .HasForeignKey(x => x.AppointmentId);

            EntityTypeBuilder<MedicalHistory> medicalHistoryFluentModel = modelBuilder.Entity<MedicalHistory>();
            medicalHistoryFluentModel.ToTable("MedicalHistories");
            medicalHistoryFluentModel.HasKey(x => x.Id);


            EntityTypeBuilder<AppointmentReminderLog> appointmentReminderLogFluentModel = modelBuilder.Entity<AppointmentReminderLog>();
            appointmentReminderLogFluentModel.ToTable("AppointmentReminderLogs");
            appointmentReminderLogFluentModel.HasKey(x => x.Id);


        }

        private void ObtainAndCheckDbAccessParameters()
        {
            if (this.appConfig is null)
            {
                throw new Exception("'HpassDbContext' cannot obtain IConfiguration through Constructor Injection!!!");
            }

            string databaseHostAdress = this.appConfig["DatabaseHost"];
            string databasePort = this.appConfig["DatabasePort"];
            string databaseSSLMode = this.appConfig["DatabaseSSLMode"];
            string databaseName = this.appConfig["DatabaseName"];
            string databaseUserName = this.appConfig["DatabaseUserName"];
            string databasePassword = this.appConfig["DatabasePassword"];
            string applicationName = this.appConfig["ApplicationName"];

            if (string.IsNullOrWhiteSpace(databaseHostAdress))
            {
                throw new Exception("'DatabaseHost' setting is not presented in appsettings.json");
            }

            this.databaseServiceAdress = databaseHostAdress.Trim();

            if (string.IsNullOrWhiteSpace(databasePort))
            {
                throw new Exception("'DatabasePort' setting is not presented in appsettings.json");
            }

            if (int.TryParse(databasePort, out int convertedServicePort))
            {
                this.databaseServicePort = convertedServicePort;
            }
            else
            {
                throw new Exception("'DatabasePort' setting in appsettings.json is not valid port number");
            }

            if (string.IsNullOrWhiteSpace(databaseSSLMode))
            {
                throw new Exception("'DatabaseSSLMode' setting is not presented in appsettings.json");
            }

            if (bool.TryParse(databaseSSLMode, out bool convertedSslMode))
            {
                this.databaseSslMode = convertedSslMode;
            }
            else
            {
                this.databaseSslMode = false;
            }

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new Exception("'databaseName' setting is not presented in appsettings.json");
            }

            this.databaseName = databaseName.Trim();


            if (string.IsNullOrWhiteSpace(databaseUserName))
            {
                throw new Exception("'DatabaseUserName' setting is not presented in appsettings.json");
            }

            this.databaseUserName = databaseUserName.Trim();


            if (string.IsNullOrWhiteSpace(databasePassword))
            {
                throw new Exception("'DatabasePassword' setting is not presented in appsettings.json");
            }

            this.databasePassword = databasePassword.Trim();


            if (string.IsNullOrWhiteSpace(applicationName))
            {
                this.applicationName = Guid.NewGuid().ToString().Replace("-", "");
            }
            else
            {
                this.applicationName = applicationName.Trim();
            }

        }


        private NpgsqlConnection GeneratePostgreSqlConnection()
        {
            return new NpgsqlConnection(this.BuildPostgresqlConnectionString());
        }

        private string BuildPostgresqlConnectionString()
        {
            var postgreSqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder();
            postgreSqlConnectionStringBuilder.Host = this.databaseServiceAdress;
            postgreSqlConnectionStringBuilder.Port = this.databaseServicePort;
            postgreSqlConnectionStringBuilder.SslMode = this.databaseSslMode ? SslMode.Allow : SslMode.Disable;
            postgreSqlConnectionStringBuilder.Database = this.databaseName;
            postgreSqlConnectionStringBuilder.Username = this.databaseUserName;
            postgreSqlConnectionStringBuilder.Password = this.databasePassword;
            postgreSqlConnectionStringBuilder.ApplicationName = this.applicationName;
            postgreSqlConnectionStringBuilder.IncludeErrorDetail = true;


            return postgreSqlConnectionStringBuilder.ConnectionString;
        }


    }
}