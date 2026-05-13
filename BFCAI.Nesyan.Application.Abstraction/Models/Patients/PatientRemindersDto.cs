using BFCAI.Nesyan.Application.Abstraction.Models.Appointments;
using BFCAI.Nesyan.Application.Abstraction.Models.Routines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Abstraction.Models.Patients
{
    public class PatientRemindersDto
    {
        public PatientSummaryDto PatientSummary { get; set; } = null!;
        public IEnumerable<AppointmentToReturnDto>? AppointmentToReturn { get; set; }
        public IEnumerable<RoutineToReturnDto>? RoutineToReturn { get; set; }
        public IEnumerable<PatientMedicationsDto>? PatientMedications { get; set; }
    }
}
