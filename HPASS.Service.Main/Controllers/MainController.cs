using Autofac;
using HPASS.Authentication.Foundation.Validation;
using HPASS.Business.Main.Abstraction;
using HPASS.Request.Common;
using HPASS.Request.Patient;
using HPASS.Service.Base.Controller;
using Microsoft.AspNetCore.Mvc;

namespace HPASS.Service.Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : BaseController
    {
        public MainController(ILifetimeScope container, IConfiguration config) : base(container, config)
        {
        }


        [HttpPost]
        [Route("CreatePatient")]
        [VerifyAccessToken]
        public IActionResult CreatePatient(CreatePatientRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.CreatePatient(request);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetDoctorsOfHealthProvider")]
        [VerifyAccessToken]
        public IActionResult GetDoctorsOfHealthProvider()
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.GetDoctorsOfHealthProvider();
            return Ok(result);
        }


        [HttpPost]
        [Route("PagingPatients")]
        [VerifyAccessToken]
        public IActionResult PagingPatients(PagingRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.PagingPatients(request);
            return Ok(result);
        }


        [HttpPost]
        [Route("UpdatePatient")]
        [VerifyAccessToken]
        public IActionResult UpdatePatient(UpdatePatientRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.UpdatePatient(request);
            return Ok(result);
        }


        [HttpPost]
        [Route("PagingOperationZones")]
        [VerifyAccessToken]
        public IActionResult PagingOperationZones(PagingRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.PagingOperationZones(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateOperationZone")]
        [VerifyAccessToken]
        public IActionResult CreateOperationZone(CreateOperationZoneRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.CreateOperationZone(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateOperationZone")]
        [VerifyAccessToken]
        public IActionResult UpdateOperationZone(UpdateOperationZoneRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.UpdateOperationZone(request);
            return Ok(result);
        }


        [HttpPost]
        [Route("AssignAppointmentToPatient")]
        [VerifyAccessToken]
        public IActionResult AssignAppointmentToPatient(AssignAppointmentToPatientRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.AssignAppointmentToPatient(request);
            return Ok(result);
        }


        [HttpPost]
        [Route("UpdateAppointmentStatus")]
        [VerifyAccessToken]
        public IActionResult UpdateAppointmentStatus(UpdateAppointmentStatusRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.UpdateAppointmentStatus(request);
            return Ok(result);
        }


        [HttpPost]
        [Route("ProvideMedicalHistoryForPatient")]
        [VerifyAccessToken]
        public IActionResult ProvideMedicalHistoryForPatient(CreateMedicalHistoryRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.ProvideMedicalHistoryForPatient(request);
            return Ok(result);
        }


        [HttpPost]
        [Route("GetAppointmentsOfPatient")]
        [VerifyAccessToken]
        public IActionResult GetAppointmentsOfPatient(ByIdRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.GetAppointmentsOfPatient(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetMedicalHistoryOfPatient")]
        [VerifyAccessToken]
        public IActionResult GetMedicalHistoryOfPatient(ByIdRequest request)
        {
            var mainBusiness = container.Resolve<IMainBusiness>();
            var result = mainBusiness.GetMedicalHistoryOfPatient(request);
            return Ok(result);
        }




    }
}
