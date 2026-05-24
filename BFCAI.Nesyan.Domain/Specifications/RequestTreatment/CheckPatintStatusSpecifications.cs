using BFCAI.Nesyan.Domain.Entities.Relations.Primary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Domain.Specifications.RequestTreatment
{
    public class CheckPatintStatusSpecifications:BaseSpecifications<RelativeDoctorRequest,int>
    {
        public CheckPatintStatusSpecifications(int patientId)
        {
            Criteria=P=>P.PatientId ==patientId && P.Status==RequestStatus.Selected;
        }
    }
}
