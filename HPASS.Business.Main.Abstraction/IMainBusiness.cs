using HPASS.Dto.Main;
using HPASS.Request.Common;
using HPASS.Request.Patient;
using HPASS.Response.Common;

namespace HPASS.Business.Main.Abstraction
{
    public interface IMainBusiness
    {


        ServiceResult<bool> GenerateDatabaseAndInitialData();
        ServiceResult<bool> CreatePatient(CreatePatientRequest request);
        ServiceResult<UserAuthenticateDto> GetUserToAuthenticate(LoginRequest request);
        ServiceResult<IEnumerable<DoctorDto>> GetDoctorsOfHealthProvider();
        ServiceResult<bool> UpdatePatient(UpdatePatientRequest request);
        ServiceResult<PagingResponse<PatientDto>> PagingPatients(PagingRequest request);
        ServiceResult<PagingResponse<OperationZoneDto>> PagingOperationZones(PagingRequest request);
        ServiceResult<bool> CreateOperationZone(CreateOperationZoneRequest request);
        ServiceResult<bool> UpdateOperationZone(UpdateOperationZoneRequest request);
        ServiceResult<bool> AssignAppointmentToPatient(AssignAppointmentToPatientRequest request);
        ServiceResult<bool> UpdateAppointmentStatus(UpdateAppointmentStatusRequest request);
        ServiceResult<bool> ProvideMedicalHistoryForPatient(CreateMedicalHistoryRequest request);
        ServiceResult<IEnumerable<AppointmentDto>> GetAppointmentsOfPatient(ByIdRequest request);
        ServiceResult<IEnumerable<MedicalHistoryDto>> GetMedicalHistoryOfPatient(ByIdRequest request);

    }
}