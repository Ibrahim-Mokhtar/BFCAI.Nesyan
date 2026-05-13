using BFCAI.Nesyan.Application.Abstraction.Models._Relations.RelativePatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Abstraction.Services._Relations
{
    public interface IRelativePatientService
    {
        public Task<RelativePatientsDto> GetRelativePatients(int relativeId);
        public Task<RelativePatientHomeDto> GetPatientHomeAsync(int relativeId, int patientId);
        //Task<IEnumerable<ReminderDto>> GetPatientRemindersAsync(int relativeId, int patientId);

        //Task<ReminderDto?> GetReminderAsync(int relativeId,int patientId, int reminderId);

        //Task<ReminderDto> CreateReminderAsync(int relativeId, CreateReminderDto dto);

        //Task UpdateReminderAsync(int relativeId,UpdateReminderDto dto);
        //Task DeleteReminderAsync(int relativeId, int reminderId);
        //Task<PatientLocationDto?> GetPatientLocationAsync(int relativeId,int patientId);
        //Task<PatientReportsDto> GetPatientReportsAsync(int relativeId,int patientId);
        //Task<PatientDetailsDto?> GetPatientDetailsAsync(int relativeId,int patientId);
    }
}
