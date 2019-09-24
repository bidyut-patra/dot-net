using sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sample.Repositories
{
    public class TrainingRepository : GenericRepository<TrainingData>, ITrainingRepository
    {
        public TrainingRepository(): base("training")
        {

        }
    }
}