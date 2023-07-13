using Autofac;
using HPASS.DataAccessLayer.Abstraction;
using HPASS.Entity.Main;

namespace HPASS.Service.Main.BackgroundServices
{
    public class AppointmentRemienderService : BackgroundService
    {
        private readonly ILifetimeScope container;
        private readonly TimeSpan appointmentCheckPeriod = TimeSpan.FromMinutes(5);


        public AppointmentRemienderService(ILifetimeScope container)
        {
            this.container = container;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            using (PeriodicTimer timer = new PeriodicTimer(this.appointmentCheckPeriod))
            {
                do
                {
                    try
                    {
                        using (var periodicLifetimeScope = this.container.BeginLifetimeScope())
                        {

                            var appointmentRepository = periodicLifetimeScope.Resolve<IRepository<Appointment>>();
                            var patientRepository = periodicLifetimeScope.Resolve<IRepository<Patient>>();

                            IEnumerable<Appointment> remindingAppointmentList = appointmentRepository.Find(x => x.Status == Enumeration.AppointmentStatusType.Created);

                            if (remindingAppointmentList is not null && remindingAppointmentList.Any())
                            {
                                foreach (var workingAppointment in remindingAppointmentList)
                                {
                                    Patient remindingPatient = patientRepository.FirstOrDefault(x => x.Id == workingAppointment.PatientId);

                                    //At that point desired message text can be formed and transmit to the proper notification channel through Kafka Event Bus

                                    this.BroadcastSmsNotificationEventBus();
                                    this.BroadcastEmailNotificationEventBus();
                                    this.BroadcastFCMNotificationEventBus();

                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        //idealy notify developers trough event bus (Kafka etc..)
                    }

                } while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken));
            }

        }


        private void BroadcastSmsNotificationEventBus()
        {
            // At that point idealy a paid subscription should be used to provide SMS reminding to patient
            // Background service will push event to Kafka event bus and consumer of sms notification channel will send the sms asynchronously paralel to this execution
            // For the sake of simplicity for this case study, further implementation is explained

        }

        private void BroadcastEmailNotificationEventBus()
        {
            // At that point idealy a paid subscription should be used to provide EMAIL reminding to patient
            // Background service will push event to Kafka event bus and consumer of EMAIL notification channel will send the EMAIL asynchronously paralel to this execution
            // For the sake of simplicity for this case study, further implementation is explained

        }

        private void BroadcastFCMNotificationEventBus()
        {
            // At that point idealy a paid subscription should be used to provide FCM (Firebase Cloud Messaging) reminding to patient
            // FCM provides asynchronous push notification capability on mobile aplications and web.
            // Background service will push event to Kafka event bus and consumer of FCM notification channel will send the FCM asynchronously paralel to this execution
            // For the sake of simplicity for this case study, further implementation is explained
        }

        

    }
}
