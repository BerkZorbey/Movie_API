using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Movie_API.Models;
using Movie_API.Models.DTOs;
using Movie_API.Models.Value_Object;

namespace Movie_API.Services
{
    public class MovieService
    {
        private readonly IMongoCollection<Movie> _movies;
        private readonly IMapper _mapper;
        public MovieService(IConfiguration configuration, IMapper mapper)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieDb");
            _movies = database.GetCollection<Movie>("Movies");
            _mapper = mapper;
        }

        public PagingResultModel<Movie> GetMovies(PagingQuery query)
        {
            PagingResultModel<Movie> movies = new PagingResultModel<Movie>(query);
            movies.GetData(_movies.AsQueryable<Movie>());
            return movies;
        }
        
        public Movie GetMovieById(string id)
        {
            return _movies.Find(x=>x.Id == id).FirstOrDefault();
        }
        public Movie AddMovie(MovieDetailDTO movie,string movie_Id)
        {
            var newMovie = _mapper.Map<Movie>(movie);
            newMovie.Id = movie_Id;
            _movies.InsertOne(newMovie);
            return newMovie;
        }
        
        public void UpdateMovie(Movie movie)
        {
          _movies.ReplaceOne(x => x.Id == movie.Id, movie);
        }
        public void DeleteMovie(string id)
        {
            _movies.DeleteOne(x => x.Id == id);
        }
    }
}
