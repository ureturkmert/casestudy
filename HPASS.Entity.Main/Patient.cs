using HPASS.Entity.Base.Implementation;
using HPASS.Enumeration;

namespace HPASS.Entity.Main
{
    public class Patient : BaseEntity
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

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<MedicalHistory> MedicalHistories { get; set; }

    }
}