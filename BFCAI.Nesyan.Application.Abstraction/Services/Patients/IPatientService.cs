using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BFCAI.Nesyan.Application.Abstraction.Models.Patients;

namespace BFCAI.Nesyan.Application.Abstraction.Services.Patients
{
    public interface IPatientService
    {
        Task UpdatePatientStageAsync(int patientId, int newStage);
        Task<PatientFullProfileDto> GetPatientProfileAsync(int patientId);
    }
}
