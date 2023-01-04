using AutoMapper;
using MongoDB.Driver;
using Movie_API.Models;

namespace Movie_API.Services
{
    public class EmailService
    {
        private readonly IMongoCollection<UserEmailVerificationModel> _userEmailVerification;
        private readonly TokenGeneratorService _tokenGeneratorService;

        public EmailService(IConfiguration configuration, TokenGeneratorService tokenGeneratorService)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("MovieMongoDb"));
            IMongoDatabase database = client.GetDatabase("MovieDb");
            _userEmailVerification = database.GetCollection<UserEmailVerificationModel>("UserEmailVerification");
            _tokenGeneratorService = tokenGeneratorService;
        }
        public async void CreateEmailVerificationToken(string id)
        {
            var emailModel = new UserEmailVerificationModel(); 
            var token = _tokenGeneratorService.GenerateToken();
            emailModel.EmailVerificationToken = token;
            emailModel.UserId = id;
            await _userEmailVerification.InsertOneAsync(emailModel);
        }
        public UserEmailVerificationModel GetEmailVerification(string id)
        {
            var emailVerification = _userEmailVerification.Find(x => x.UserId == id).FirstOrDefault();
            return emailVerification;
        }
    }
}
