using sample.Models;
using sample.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace sample.Controllers
{
    [RoutePrefix("trainings")]
    public class TrainingController : ApiController
    {
        public ITrainingService TrainingService { get; set; }

        public TrainingController(ITrainingService trainingService)
        {
            this.TrainingService = trainingService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllTrainingData()
        {
            var result = await this.TrainingService.GetAllTrainingData();
            return Ok(result);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetTrainingData(int id)
        {
            var result = await Task.Run(() => new TrainingData());
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> SaveTrainingData([FromBody] TrainingData trainingData)
        {
            var result = await this.TrainingService.SaveTrainingData(trainingData);
            return Ok(result);
        }
    }
}
