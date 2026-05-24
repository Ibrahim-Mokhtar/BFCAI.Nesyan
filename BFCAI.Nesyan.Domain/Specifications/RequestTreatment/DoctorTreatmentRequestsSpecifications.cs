using BFCAI.Nesyan.Domain.Entities.Relations.Primary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Domain.Specifications.RequestTreatment
{
    public class DoctorTreatmentRequestsSpecifications:BaseSpecifications<RelativeDoctorRequest,int>
    {
        public DoctorTreatmentRequestsSpecifications(int doctorId)
        {
            Criteria = P => P.DoctorId == doctorId && (P.Status==RequestStatus.Pending| P.Status == RequestStatus.Accepted);
            AddStringinclude("Doctor");
            AddStringinclude("Patient.Assessments");
            AddStringinclude("Relative");
        }
    }
}
