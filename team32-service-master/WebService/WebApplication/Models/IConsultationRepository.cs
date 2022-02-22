using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public interface IConsultationRepository
    {
        List<ConsultationModel> Consultations();
        ConsultationModel AddConsultation(ConsultationModel consultationModel);
        ConsultationModel GetConsultation(int consultationId);
        ConsultationModel UpdateConsultation(int consultationId, ConsultationModel consultationModel);
        ConsultationModel DeleteConsultation(int consultationId);
    }
}
