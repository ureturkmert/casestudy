using AutoMapper;
using HPASS.Dto.Main;
using HPASS.Entity.Main;
using HPASS.Request.Patient;
using HPASS.Response.Common;

namespace HPASS.Service.AutoMapperProfile
{
    public class MappingProfiler : Profile
    {

        public MappingProfiler()
        {
            this.CreateMap<Patient, PatientDto>();
            this.CreateMap<CreatePatientRequest, Patient>();
            this.CreateMap<Doctor, DoctorDto>();
            this.CreateMap<User, UserDto>();
            this.CreateMap<User, UserAuthenticateDto>();
            this.CreateMap<Department, DepartmentDto>();
            this.CreateMap<HealthcareProvider, HealthcareProviderDto>();
            this.CreateMap<OperationZone, OperationZoneDto>();
            this.CreateMap<PagingResponse<Patient>, PagingResponse<PatientDto>>();
            this.CreateMap<PagingResponse<OperationZone>, PagingResponse<OperationZoneDto>>();
            this.CreateMap<CreateOperationZoneRequest, OperationZone>();
            this.CreateMap<MedicalHistory, MedicalHistoryDto>();
            this.CreateMap<Appointment, AppointmentDto>();
        }
    }
}
