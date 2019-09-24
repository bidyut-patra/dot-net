using sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample.Services
{
    public interface ITrainingService
    {
        Task<List<TrainingData>> GetAllTrainingData();
        Task<TrainingData> SaveTrainingData(TrainingData trainingData);
    }
}
