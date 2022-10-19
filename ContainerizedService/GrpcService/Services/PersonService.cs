using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService
{
    public class PersonService : PersonDetails.PersonDetailsBase
    {
        private readonly ILogger<PersonService> _logger;
        public PersonService(ILogger<PersonService> logger)
        {
            _logger = logger;
        }

        public override Task<Person> GetPerson(PersonRequest request, ServerCallContext context)
        {
            return Task.FromResult(new Person
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Bidyut Kumar Patra",
                Address = new Address()
                {
                    House = "405, Radiant Shine",
                    Street = "Yellenahalli, Begur Koppa Road",
                    City = "Bangalore",
                    Country = "INDIA",
                    Pincode = "560068"
                },
                Education = new EducationInfo()
                {
                    HighestQualification = "BE in Information Technology",
                    YearOfPassout = Timestamp.FromDateTime(new DateTime(2004, 6, 30, 0, 0, 0, DateTimeKind.Utc)),
                }
            });
        }
    }
}
