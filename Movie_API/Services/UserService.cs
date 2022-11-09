using AutoMapper;
using MongoDB.Driver;
using Movie_API.Models;

namespace Movie_API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<Movie> _movies;
        private readonly IMapper _mapper;
        public UserService(IConfiguration configuration, IMapper mapper)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieUserDb");
            _movies = database.GetCollection<Movie>("Users");
            _mapper = mapper;
        }

    }
}
