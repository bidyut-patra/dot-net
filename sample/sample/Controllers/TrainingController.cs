using sample.Exceptions;
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

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> SaveTrainingData([FromBody] TrainingData trainingData)
        {
            if (ModelState.IsValid)
            {
                var result = await this.TrainingService.SaveTrainingData(trainingData);
                return Ok(result);
            }
            else
            {
                throw new ModelInvalidException();
            }
        }
    }
}
