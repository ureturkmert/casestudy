using Autofac;
using HPASS.Business.Base;
using HPASS.Business.Main.Abstraction;
using HPASS.Crosscutting.Abstraction;
using HPASS.Crosscutting.Helper;
using HPASS.DataAccessLayer.Abstraction;
using HPASS.Dto.Main;
using HPASS.Entity.Base.Implementation;
using HPASS.Entity.Main;
using HPASS.Enumeration;
using HPASS.Request.Common;
using HPASS.Request.Patient;
using HPASS.Response.Common;
using System.Reflection;

namespace HPASS.Business.Main.Implementation
{
    public class MainBusiness : BaseBusiness, IMainBusiness
    {
        public MainBusiness(ILifetimeScope container) : base(container)
        {
        }

        private void MergeRequestOnEntity<T, Q>(T sourceEntity, Q request) where T : BaseEntity where Q : class
        {
            PropertyInfo[] sourceEntityProperties = typeof(T).GetProperties();
            PropertyInfo[] requestProperties = typeof(Q).GetProperties();

            foreach (var attemptingRequestProperty in requestProperties)
            {
                PropertyInfo entityProperty = sourceEntityProperties.FirstOrDefault(x => x.Name == attemptingRequestProperty.Name);
                if (entityProperty is null)
                {
                    continue;
                }
                entityProperty.SetValue(sourceEntity, attemptingRequestProperty.GetValue(request), null);
            }
        }

        private void GenerateDefaultHealtcareProviders()
        {
            var hpRepository = this.container.Resolve<IRepository<HealthcareProvider>>();
            var opRepository = this.container.Resolve<IRepository<OperationZone>>();
            var userRepository = this.container.Resolve<IRepository<User>>();
            var departmentRepository = this.container.Resolve<IRepository<Department>>();
            var doctorRepository = this.container.Resolve<IRepository<Doctor>>();

            HealthcareProvider acibadem = new HealthcareProvider();
            acibadem.Name = "Acibadem Healthcare Group";
            hpRepository.Attach(acibadem);

            OperationZone acibademFirstOperationZone = new OperationZone();
            acibademFirstOperationZone.Name = "Acıbadem İstanbul";
            acibademFirstOperationZone.HealthcareProviderId = acibadem.Id;
            opRepository.Attach(acibademFirstOperationZone);

            OperationZone acibademSecondOperationZone = new OperationZone();
            acibademSecondOperationZone.Name = "Acıbadem Ankara";
            acibademSecondOperationZone.HealthcareProviderId = acibadem.Id;
            opRepository.Attach(acibademSecondOperationZone);


            HealthcareProvider medicalPark = new HealthcareProvider();
            medicalPark.Name = "Medical Park";
            hpRepository.Attach(medicalPark);

            OperationZone medicalParkFirstOperationZone = new OperationZone();
            medicalParkFirstOperationZone.Name = "Medical Park İstanbul";
            medicalParkFirstOperationZone.HealthcareProviderId = medicalPark.Id;
            opRepository.Attach(medicalParkFirstOperationZone);

            OperationZone medicalParkSecondOperationZone = new OperationZone();
            medicalParkSecondOperationZone.Name = "Medical Park Ankara";
            medicalParkSecondOperationZone.HealthcareProviderId = medicalPark.Id;
            opRepository.Attach(medicalParkSecondOperationZone);


            HealthcareProvider guven = new HealthcareProvider();
            guven.Name = "Güven";
            hpRepository.Attach(guven);

            OperationZone guvenFirstOperationZone = new OperationZone();
            guvenFirstOperationZone.Name = "GÜVEN Hastanesi";
            guvenFirstOperationZone.HealthcareProviderId = guven.Id;
            opRepository.Attach(guvenFirstOperationZone);

            OperationZone guvenSecondOperationZone = new OperationZone();
            guvenSecondOperationZone.Name = "GÜVEN Çayyolu Cerrahi Tıp Merkezi";
            guvenSecondOperationZone.HealthcareProviderId = guven.Id;
            opRepository.Attach(guvenSecondOperationZone);

            OperationZone guvenThirdOperationZone = new OperationZone();
            guvenThirdOperationZone.Name = "GÜVEN Çayyolu Sağlıklı Yaşam Kampüsü";
            guvenThirdOperationZone.HealthcareProviderId = guven.Id;
            opRepository.Attach(guvenThirdOperationZone);


            User acibademUser = new User();
            acibademUser.Name = "acibadem";
            acibademUser.Surname = "test";
            acibademUser.Username = "acibademtest";
            acibademUser.HealthcareProviderId = acibadem.Id;
            acibademUser.Password = PasswordHashUtility.GenerateHash("acibademtest");
            userRepository.Attach(acibademUser);

            User medicalParkUser = new User();
            medicalParkUser.Name = "medicalpark";
            medicalParkUser.Surname = "test";
            medicalParkUser.Username = "medicalparktest";
            medicalParkUser.HealthcareProviderId = medicalPark.Id;
            medicalParkUser.Password = PasswordHashUtility.GenerateHash("medicalparktest");
            userRepository.Attach(medicalParkUser);

            User guvenUser = new User();
            guvenUser.Name = "guven";
            guvenUser.Surname = "test";
            guvenUser.Username = "guventest";
            guvenUser.HealthcareProviderId = guven.Id;
            guvenUser.Password = PasswordHashUtility.GenerateHash("guventest");
            userRepository.Attach(guvenUser);


            Department eye = new Department();
            eye.Name = "Göz Hastalıkları";
            departmentRepository.Attach(eye);

            Department kbb = new Department();
            kbb.Name = "Kulak Burun Boğaz";
            departmentRepository.Attach(kbb);

            Doctor guvenDoctorA = new Doctor();
            guvenDoctorA.Name = "Ali Hakan";
            guvenDoctorA.Surname = "Durukan";
            guvenDoctorA.Title = "Prof. Dr.";
            guvenDoctorA.HealthcareProviderId = guven.Id;
            guvenDoctorA.DepartmentId = eye.Id;
            doctorRepository.Attach(guvenDoctorA);

        }

        private void GenerateDefaultPatients()
        {

            var userRepository = this.container.Resolve<IRepository<Patient>>();

            Patient firstDefaultPatient = new Patient();
            firstDefaultPatient.Name = "Test1";
            firstDefaultPatient.Surname = "Patient1";
            firstDefaultPatient.Age = 21;
            firstDefaultPatient.GenderType = GenderTypes.Female;
            firstDefaultPatient.NationalIdentifier = "11111111111";
            firstDefaultPatient.Phone = "05351231212";
            firstDefaultPatient.Email = "test@gmail.com";
            firstDefaultPatient.Heigth = 180;
            firstDefaultPatient.Weight = 70;


            userRepository.Attach(firstDefaultPatient);


            Patient secondDefaultPatient = new Patient();
            secondDefaultPatient.Name = "Test2";
            secondDefaultPatient.Surname = "Patient2";
            secondDefaultPatient.Age = 22;
            secondDefaultPatient.GenderType = GenderTypes.Male;
            secondDefaultPatient.NationalIdentifier = "22222222222";
            secondDefaultPatient.Phone = "05351231212";
            secondDefaultPatient.Email = "test@gmail.com";
            secondDefaultPatient.Heigth = 150;
            secondDefaultPatient.Weight = 45;

            userRepository.Attach(secondDefaultPatient);

            Patient thirdDefaultPatient = new Patient();
            thirdDefaultPatient.Name = "Test3";
            thirdDefaultPatient.Surname = "Patient3";
            thirdDefaultPatient.Age = 23;
            thirdDefaultPatient.GenderType = GenderTypes.Unkown;
            thirdDefaultPatient.NationalIdentifier = "33333333333";
            thirdDefaultPatient.Phone = "05351231212";
            thirdDefaultPatient.Email = "test@gmail.com";
            secondDefaultPatient.Heigth = 160;
            secondDefaultPatient.Weight = 85;

            userRepository.Attach(thirdDefaultPatient);

        }

        public ServiceResult<bool> GenerateDatabaseAndInitialData()
        {
            var userRepository = this.container.Resolve<IRepository<Patient>>();
            userRepository.EnsureDatabaseDesignCompatibility();

            this.GenerateDefaultHealtcareProviders();
            this.GenerateDefaultPatients();

            this.UnitOfWork.Commit();
            return new ServiceResult<bool>(true, "");
        }

        public ServiceResult<PagingResponse<PatientDto>> PagingPatients(PagingRequest request)
        {
            var patientRepository = this.container.Resolve<IRepository<Patient>>();

            PagingResponse<Patient> response = patientRepository.Paging(request.PageNumber, request.PageSize);

            return new ServiceResult<PagingResponse<PatientDto>>(this.MappingManager.Map<PagingResponse<Patient>, PagingResponse<PatientDto>>(response), "");

        }

        public ServiceResult<bool> CreatePatient(CreatePatientRequest request)
        {
            var patientRepository = this.container.Resolve<IRepository<Patient>>();

            Patient existingUsernamePatient = patientRepository.FirstOrDefault(x => x.NationalIdentifier == request.NationalIdentifier);

            if (existingUsernamePatient != null)
            {
                return new ServiceResult<bool>("PatientExist");
            }

            Patient addingPatient = this.MappingManager.Map<CreatePatientRequest, Patient>(request);
            patientRepository.Attach(addingPatient);

            this.UnitOfWork.Commit();

            return new ServiceResult<bool>(true, "");
        }

        public ServiceResult<bool> UpdatePatient(UpdatePatientRequest request)
        {
            var patientRepository = this.container.Resolve<IRepository<Patient>>();

            Patient updatingPatient = patientRepository.FirstOrDefault(x => x.Id == request.UpdatingPatientId);
            if (updatingPatient is null)
            {
                return new ServiceResult<bool>("PATIENT_404");
            }

            this.MergeRequestOnEntity<Patient, UpdatePatientRequest>(updatingPatient, request);
            patientRepository.Update(updatingPatient);

            this.UnitOfWork.Commit();

            return new ServiceResult<bool>(true, "");

        }

        public ServiceResult<UserAuthenticateDto> GetUserToAuthenticate(LoginRequest request)
        {
            var userRepository = this.container.Resolve<IRepository<User>>();
            var hpRepository = this.container.Resolve<IRepository<HealthcareProvider>>();

            User loginAttemptingUser = userRepository.FirstOrDefault(x => x.Username == request.Username);
            if (loginAttemptingUser == null)
            {
                return new ServiceResult<UserAuthenticateDto>("USR_404");
            }

            UserAuthenticateDto returningDto = this.MappingManager.Map<User, UserAuthenticateDto>(loginAttemptingUser);
            HealthcareProvider correspondingProvider = hpRepository.FirstOrDefault(x => x.Id == loginAttemptingUser.HealthcareProviderId);
            returningDto.HealthcareProvider = this.MappingManager.Map<HealthcareProvider, HealthcareProviderDto>(correspondingProvider);

            return new ServiceResult<UserAuthenticateDto>(returningDto, "");

        }

        public ServiceResult<IEnumerable<DoctorDto>> GetDoctorsOfHealthProvider()
        {

            var userSessionContext = this.container.Resolve<IUserSessionContext>();

            if (userSessionContext is null || userSessionContext.HPID == Guid.Empty)
            {
                return new ServiceResult<IEnumerable<DoctorDto>>("INVALID_SESSION");
            }

            var doctorRepository = this.container.Resolve<IRepository<Doctor>>();
            var departmentRepository = this.container.Resolve<IRepository<Department>>();

            IEnumerable<Doctor> foundDoctors = doctorRepository.Find(x => x.HealthcareProviderId == userSessionContext.HPID);


            IList<DoctorDto> returningDoctorList = new List<DoctorDto>();

            foreach (var iteratingDoctor in foundDoctors)
            {
                Department foundDepartment = departmentRepository.FirstOrDefault(x => x.Id == iteratingDoctor.DepartmentId);

                DoctorDto addingDoctor = this.MappingManager.Map<Doctor, DoctorDto>(iteratingDoctor);
                addingDoctor.Department = this.MappingManager.Map<Department, DepartmentDto>(foundDepartment);
                returningDoctorList.Add(addingDoctor);
            }


            return new ServiceResult<IEnumerable<DoctorDto>>(returningDoctorList, "");
        }

        public ServiceResult<PagingResponse<OperationZoneDto>> PagingOperationZones(PagingRequest request)
        {
            var userSessionContext = this.container.Resolve<IUserSessionContext>();
            if (userSessionContext is null || userSessionContext.HPID == Guid.Empty)
            {
                return new ServiceResult<PagingResponse<OperationZoneDto>>("INVALID_SESSION");
            }

            var operationZoneRepository = this.container.Resolve<IRepository<OperationZone>>();

            PagingResponse<OperationZone> response = operationZoneRepository
                .Paging(request.PageNumber, request.PageSize, x => x.HealthcareProviderId == userSessionContext.HPID);

            return new ServiceResult<PagingResponse<OperationZoneDto>>(this.MappingManager.Map<PagingResponse<OperationZone>, PagingResponse<OperationZoneDto>>(response), "");


        }

        public ServiceResult<bool> CreateOperationZone(CreateOperationZoneRequest request)
        {
            var userSessionContext = this.container.Resolve<IUserSessionContext>();

            if (userSessionContext is null || userSessionContext.HPID == Guid.Empty)
            {
                return new ServiceResult<bool>("INVALID_SESSION");
            }

            var operationZoneRepository = this.container.Resolve<IRepository<OperationZone>>();

            OperationZone existingWithSameName = operationZoneRepository.FirstOrDefault(x => x.Name == request.Name);
            if (existingWithSameName is not null)
            {
                return new ServiceResult<bool>("OPERATION_ZONE_EXIST");
            }

            OperationZone addingOperationZone = this.MappingManager.Map<CreateOperationZoneRequest, OperationZone>(request);
            addingOperationZone.HealthcareProviderId = userSessionContext.HPID;
            operationZoneRepository.Attach(addingOperationZone);

            this.UnitOfWork.Commit();

            return new ServiceResult<bool>(true, "");
        }

        public ServiceResult<bool> UpdateOperationZone(UpdateOperationZoneRequest request)
        {

            var userSessionContext = this.container.Resolve<IUserSessionContext>();

            if (userSessionContext is null || userSessionContext.HPID == Guid.Empty)
            {
                return new ServiceResult<bool>("INVALID_SESSION");
            }

            var operationZoneRepository = this.container.Resolve<IRepository<OperationZone>>();

            OperationZone updatingZone = operationZoneRepository.FirstOrDefault(x => x.Id == request.UpdatingOperationZoneId);
            if (updatingZone is null)
            {
                return new ServiceResult<bool>("OPERATION_ZONE_404");
            }

            OperationZone existingOZWithSameName = operationZoneRepository.FirstOrDefault(x => x.Id != updatingZone.Id && x.Name == request.Name);
            if (existingOZWithSameName is not null)
            {
                return new ServiceResult<bool>("NAME_IN_USE");
            }

            this.MergeRequestOnEntity<OperationZone, UpdateOperationZoneRequest>(updatingZone, request);
            operationZoneRepository.Update(updatingZone);

            this.UnitOfWork.Commit();

            return new ServiceResult<bool>(true, "");

        }

        public ServiceResult<bool> AssignAppointmentToPatient(AssignAppointmentToPatientRequest request)
        {
            var userSessionContext = this.container.Resolve<IUserSessionContext>();

            if (userSessionContext is null || userSessionContext.HPID == Guid.Empty)
            {
                return new ServiceResult<bool>("INVALID_SESSION");
            }


            var patientRepository = this.container.Resolve<IRepository<Patient>>();
            var doctorRepository = this.container.Resolve<IRepository<Doctor>>();
            var operationzoneRepository = this.container.Resolve<IRepository<OperationZone>>();
            var appointmentRepository = this.container.Resolve<IRepository<Appointment>>();

            Patient workingPatient = patientRepository.FirstOrDefault(x => x.Id == request.PatientId);
            if (workingPatient is null)
            {
                return new ServiceResult<bool>("PATIENT_404");
            }

            Doctor workingDoctor = doctorRepository.FirstOrDefault(x => x.Id == request.DoctorId);
            if (workingDoctor is null)
            {
                return new ServiceResult<bool>("DOCTOR_404");
            }

            if (workingDoctor.HealthcareProviderId != userSessionContext.HPID)
            {
                return new ServiceResult<bool>("DOCTOR_HEALCARE_PROVIDER_MISS_MATCH");
            }


            OperationZone workingOperationZone = operationzoneRepository.FirstOrDefault(x => x.Id == request.OperationZoneId);
            if (workingOperationZone is null)
            {
                return new ServiceResult<bool>("OPERATION_ZONE_404");
            }

            if (workingOperationZone.HealthcareProviderId != userSessionContext.HPID)
            {
                return new ServiceResult<bool>("OPERATION_ZONE_HEALCARE_PROVIDER_MISS_MATCH");
            }


            Appointment occupiedAppointment = appointmentRepository.FirstOrDefault(x => x.DoctorId == workingDoctor.Id && x.Status == AppointmentStatusType.Created && x.Date == request.Date);

            if (occupiedAppointment is not null)
            {
                return new ServiceResult<bool>("DOCTOR_BUSY");
            }

            Appointment addingAppointment = new Appointment();
            addingAppointment.Date = request.Date;
            addingAppointment.Status = AppointmentStatusType.Created;
            addingAppointment.PatientId = workingPatient.Id;
            addingAppointment.HealthcareProviderId = userSessionContext.HPID;
            addingAppointment.DoctorId = workingDoctor.Id;
            addingAppointment.DepartmentId = workingDoctor.DepartmentId;
            addingAppointment.OperationZoneId = workingOperationZone.Id;
            appointmentRepository.Attach(addingAppointment);

            this.UnitOfWork.Commit();

            return new ServiceResult<bool>(true, "");

        }

        public ServiceResult<bool> UpdateAppointmentStatus(UpdateAppointmentStatusRequest request)
        {
            var appointmentRepository = this.container.Resolve<IRepository<Appointment>>();

            Appointment workingAppointment = appointmentRepository.FirstOrDefault(x => x.Id == request.AppointmentId);
            if (workingAppointment is null)
            {
                return new ServiceResult<bool>("APPOINTMENT_404");
            }

            workingAppointment.Status = request.NewStatus;
            appointmentRepository.Update(workingAppointment);

            this.UnitOfWork.Commit();

            return new ServiceResult<bool>(true, "");

        }

        public ServiceResult<bool> ProvideMedicalHistoryForPatient(CreateMedicalHistoryRequest request)
        {
            var patientRepository = this.container.Resolve<IRepository<Patient>>();
            var medicalHistoryRepository = this.container.Resolve<IRepository<MedicalHistory>>();

            Patient workingPatient = patientRepository.FirstOrDefault(x => x.Id == request.PatientId);
            if (workingPatient is null)
            {
                return new ServiceResult<bool>("PATIENT_404");
            }

            MedicalHistory addingMedicalHistory = new MedicalHistory();
            addingMedicalHistory.Header = request.Header;
            addingMedicalHistory.Description = request.Description;
            addingMedicalHistory.PatientId = workingPatient.Id;
            medicalHistoryRepository.Attach(addingMedicalHistory);

            this.UnitOfWork.Commit();

            return new ServiceResult<bool>(true, "");

        }

        public ServiceResult<IEnumerable<AppointmentDto>> GetAppointmentsOfPatient(ByIdRequest request)
        {
            var appointmentRepository = this.container.Resolve<IRepository<Appointment>>();
            var patientRepository = this.container.Resolve<IRepository<Patient>>();
            var doctorRepository = this.container.Resolve<IRepository<Doctor>>();
            var operationZoneRepository = this.container.Resolve<IRepository<OperationZone>>();
            var departmentRepository = this.container.Resolve<IRepository<Department>>();
            var healthcareProviderRepository = this.container.Resolve<IRepository<HealthcareProvider>>();

            IList<AppointmentDto> returningList = new List<AppointmentDto>();

            IEnumerable<Appointment> foundAppointments = appointmentRepository.Find(x => x.PatientId == request.Id);

            if (foundAppointments is null || !foundAppointments.Any())
            {
                return new ServiceResult<IEnumerable<AppointmentDto>>(returningList, "");
            }

            foreach (var iteratingAppointment in foundAppointments)
            {
                AppointmentDto addingDto = this.MappingManager.Map<Appointment, AppointmentDto>(iteratingAppointment);

                addingDto.Patient = this.MappingManager.Map<Patient, PatientDto>(patientRepository.FirstOrDefault(x => x.Id == iteratingAppointment.PatientId));
                addingDto.Doctor = this.MappingManager.Map<Doctor, DoctorDto>(doctorRepository.FirstOrDefault(x => x.Id == iteratingAppointment.DoctorId));
                addingDto.OperationZone = this.MappingManager.Map<OperationZone, OperationZoneDto>(operationZoneRepository.FirstOrDefault(x => x.Id == iteratingAppointment.OperationZoneId));
                addingDto.Department = this.MappingManager.Map<Department, DepartmentDto>(departmentRepository.FirstOrDefault(x => x.Id == iteratingAppointment.DepartmentId));
                addingDto.HealthcareProvider = this.MappingManager.Map<HealthcareProvider, HealthcareProviderDto>(healthcareProviderRepository.FirstOrDefault(x => x.Id == iteratingAppointment.HealthcareProviderId));

                returningList.Add(addingDto);

            }

            return new ServiceResult<IEnumerable<AppointmentDto>>(returningList, "");

        }

        public ServiceResult<IEnumerable<MedicalHistoryDto>> GetMedicalHistoryOfPatient(ByIdRequest request)
        {
            var medicalHistoryRepository = this.container.Resolve<IRepository<MedicalHistory>>();

            IEnumerable<MedicalHistory> foundHistory = medicalHistoryRepository.Find(x => x.PatientId == request.Id);

            return new ServiceResult<IEnumerable<MedicalHistoryDto>>(this.MappingManager.Map<IEnumerable<MedicalHistory>, IEnumerable<MedicalHistoryDto>>(foundHistory), "");

        }

    }
}