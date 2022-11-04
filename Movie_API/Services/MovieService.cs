using MongoDB.Bson;
using MongoDB.Driver;
using Movie_API.Models;
using Movie_API.Models.Value_Object;

namespace Movie_API.Services
{
    public class MovieService
    {
        private readonly IMongoCollection<Movie> _movies;
        public MovieService(IConfiguration configuration)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieDb");
            _movies = database.GetCollection<Movie>("Movies");
        }

        public List<Movie> GetMovies(PagingQuery query)
        {
            PagingResultModel<Movie> movies = new PagingResultModel<Movie>(query);
            movies.GetData(_movies.AsQueryable<Movie>());
            return movies;
        } 
        public Movie GetMovieById(string id)
        {
            return _movies.Find(x=>x.Id == id).FirstOrDefault();
        }
        public Movie AddMovie(Movie movie,string movie_Id)
        {
            Movie newMovie = new Movie()
            {
                Id = movie_Id,
                Type = movie.Type,
                Title = movie.Title,
                Cast = movie.Cast,
                Date_Added = movie.Date_Added,
                Description = movie.Description,
                Country = movie.Country,
                Rating = movie.Rating,
                Director = movie.Director,
                Duration = movie.Duration,
                Listed_In = movie.Listed_In,
                Release_Year = movie.Release_Year,
            };
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
