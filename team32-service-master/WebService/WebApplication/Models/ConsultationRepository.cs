using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class ConsultationRepository: IConsultationRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly IModelFactory _modelFactory;

        public ConsultationRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(_db);
        }

        public List<ConsultationModel> Consultations()
        {
            var consultationModels = new List<ConsultationModel>();
            var consultations = _db.Consultations;

            foreach (var consultation in consultations)
            {
                consultationModels.Add((ConsultationModel)
                    _modelFactory.CreateConsultationModel(consultation));
            }

            return consultationModels;
        }

        public ConsultationModel AddConsultation(ConsultationModel consultationModel)
        {
            var consultation = new Consultation()
            {
                Service_Id = consultationModel.ServiceId,
                Teller = consultationModel.Teller,
                Status = consultationModel.Status,
                Start_At = consultationModel.StartAt,
                End_At = consultationModel.EndAt,
                Duration = consultationModel.Duration,
                Last_Update = consultationModel.LastUpdate,
                Initial_Date = consultationModel.InitialDate
            };

            _db.Consultations.InsertOnSubmit(consultation);
            _db.SubmitChanges();

            return (ConsultationModel)
                _modelFactory.CreateConsultationModel(consultation);
        }

        public ConsultationModel GetConsultation(int consultationId)
        {
            var consultation = _db.Consultations.FirstOrDefault(x => x.Consultation_Id == consultationId);
            if (consultation == null) return null;
            return (ConsultationModel)
                _modelFactory.CreateConsultationModel(consultation);
        }

        public ConsultationModel UpdateConsultation(int consultationId, ConsultationModel consultationModel)
        {
            var consultation = _db.Consultations.FirstOrDefault(x => x.Consultation_Id == consultationId);

            if (consultation == null) return null;

            consultation.Service_Id = consultationModel.ServiceId;
            consultation.Teller = consultationModel.Teller;
            consultation.Status = consultationModel.Status;
            consultation.Start_At = consultationModel.StartAt;
            consultation.End_At = consultationModel.EndAt;
            consultation.Duration = consultationModel.Duration;
            consultation.Last_Update = consultationModel.GetLastUpdateDate();

            _db.SubmitChanges();

            return (ConsultationModel)_modelFactory.CreateConsultationModel(consultation);
        }

        public ConsultationModel DeleteConsultation(int consultationId)
        {
            var consultation = _db.Consultations.FirstOrDefault(x => x.Consultation_Id == consultationId);

            if (consultation == null) return null;

            _db.Consultations.DeleteOnSubmit(consultation);
            _db.SubmitChanges();

            return (ConsultationModel)
                _modelFactory.CreateConsultationModel(consultation);
        }
    }
}