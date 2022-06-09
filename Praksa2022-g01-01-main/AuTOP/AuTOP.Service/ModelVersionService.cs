using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Model.DomainModels;
using AuTOP.Repository;
using AuTOP.Repository.Common;
using AuTOP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Service
{
    class ModelVersionService : IModelVersionService
    {
        private IModelVersionRepository modelVersionRepository;
        private IModelRepository modelRepository;
        private IManufacturerRepository manufacturerRepository;
        private IMotorRepository motorRepository;
        private ITransmissionRepository transmissionRepository;
        private IBodyShapeRepository bodyShapeRepository;
        private IReviewService reviewService;
        private IReactionRepository reactionRepository;
        private IUserRepository userRepository;
        public ModelVersionService(IModelVersionRepository modelVersionRepository, IModelRepository modelRepository, IManufacturerRepository manufacturerRepository ,IMotorRepository motorRepository, ITransmissionRepository transmissionRepository, IBodyShapeRepository bodyShapeRepository,IReviewService reviewService,IReactionRepository reactionRepository,IUserRepository userRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
            this.modelVersionRepository = modelVersionRepository;
            this.modelRepository = modelRepository;
            this.motorRepository = motorRepository;
            this.transmissionRepository = transmissionRepository;
            this.bodyShapeRepository = bodyShapeRepository;
            this.reviewService = reviewService;
            this.reactionRepository = reactionRepository;
            this.userRepository = userRepository;
            
        }
        public async Task<List<ModelVersion>> GetAllModelVersionsAsync(ModelVersionFilter filter, Sorting sort, Paging paging)
        {
            List<ModelVersion> ModelVersions = await modelVersionRepository.GetAllModelVersions(filter, sort, paging);
            foreach (ModelVersion modelVersion in ModelVersions)
            {
                modelVersion.Model = await modelRepository.GetModelById(modelVersion.ModelId);
                modelVersion.Model.Manufacturer = await manufacturerRepository.GetManufacturerByIdAsync(modelVersion.Model.ManufacturerId);
            }

            return ModelVersions;
        }
        public async Task<ModelVersion> GetModelVersionByIdAsync(Guid id,string currentUserName)
        {
            var total = 0.00;
            ModelVersion domainModelVersion = await modelVersionRepository.GetModelVersionById(id);
            domainModelVersion.Model = await modelRepository.GetModelById(domainModelVersion.ModelId);
            domainModelVersion.Model.Manufacturer = await manufacturerRepository.GetManufacturerByIdAsync(domainModelVersion.Model.ManufacturerId);
            domainModelVersion.Motor = await motorRepository.GetByIdAsync(domainModelVersion.MotorId);
            domainModelVersion.Transmission = await transmissionRepository.GetByIdAsync(domainModelVersion.TransmissionId);
            domainModelVersion.BodyShape = await bodyShapeRepository.GetByIdAsync(domainModelVersion.BodyShapeId);
            domainModelVersion.Reviews = await reviewService.GetAsync(new ReviewFilter { ModelVersionId=domainModelVersion.Id}, new Sorting("",""), new Paging(true));
            Guid userId = Guid.Empty;
            if (currentUserName != "") {
                userId = await userRepository.GetIdbyName(currentUserName);
            }
            
            foreach(Review review in domainModelVersion.Reviews)
            {
                if (currentUserName != "")
                {
                    review.CurrentUserReaction = await reactionRepository.GetUserReaction(userId, review.Id);
                }
                total += review.Rating; 
            }
            domainModelVersion.TotalRating = Math.Round((total / domainModelVersion.Reviews.Count) * 2, MidpointRounding.AwayFromZero) / 2;
            
            return domainModelVersion;
        }

        public async Task PostModelVersionAsync(ModelVersion model)
        {
            model.Generate();
            await modelVersionRepository.PostModelVersionAsync(model);
        }
        public async Task PutModelVersionAsync(ModelVersion model)
        {
            model.DateUpdated = DateTime.UtcNow;
            await modelVersionRepository.PostModelVersionAsync(model);
        }
        public async Task DeleteModelVersionAsync(Guid id)
        {
            await modelVersionRepository.DeleteModelVersionAsync(id);
        }
    }
}
