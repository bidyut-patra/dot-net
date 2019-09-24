using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sample.Exceptions;
using sample.Models;
using sample.Repositories;

namespace sample.Services
{
    public class TrainingService : ITrainingService
    {
        public ITrainingRepository TrainingRepository { get; set; }

        public TrainingService(ITrainingRepository trainingRepository)
        {
            this.TrainingRepository = trainingRepository;
        }

        public async Task<List<TrainingData>> GetAllTrainingData()
        {
            return await this.TrainingRepository.GetAsync();
        }

        public async Task<TrainingData> SaveTrainingData(TrainingData trainingData)
        {
            if(trainingData.StartDate > trainingData.EndDate)
            {
                throw new CompareDateException();
            }
            else
            {
                return await this.TrainingRepository.SaveAsync(trainingData);
            }
        }
    }
}