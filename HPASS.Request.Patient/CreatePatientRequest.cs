using HPASS.Enumeration;

namespace HPASS.Request.Patient
{
    public class CreatePatientRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string NationalIdentifier { get; set; }
        public GenderTypes GenderType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Weight { get; set; }
        public int Heigth { get; set; }
    }
}