using StudentOrganizer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentOrganizer.Core.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using StudentOrganizer.Infrastructure.Settings;
using MongoDB.Driver.Linq;
using System.Linq;

namespace StudentOrganizer.Infrastructure.Mongo.Repositories
{
	public class MongoUserRepository : IUserRepository
	{
		private readonly IMongoDatabase _database;
		private IMongoCollection<User> Users => _database.GetCollection<User>("Users");

		public MongoUserRepository(IConfiguration configuration)
		{
			var mongoSettings = new MongoSettings();
			configuration.GetSection("mongo").Bind(mongoSettings);
			mongoSettings.ConnectionString = 
				mongoSettings.ConnectionString.Replace("<password>", configuration["MongoDbPassword"]);
			var mongoClient = new MongoClient(mongoSettings.ConnectionString);
			_database = mongoClient.GetDatabase(mongoSettings.Database);
		}

		public async Task AddAsync(User user)
		{			
			await Users.InsertOneAsync(user);
		}		

		public async Task DeleteAsync(Guid id)
		{
			await Users.DeleteOneAsync(u => u.Id == id);
		}

		public async Task<User> GetAsync(Guid id)
		{
			return await Users.AsQueryable().FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<User> GetAsync(string email)
		{
			return await Users.AsQueryable().FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task UpdateAsync(User user)
		{
			await Users.ReplaceOneAsync(u => u.Id == user.Id, user);
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(User user)
		{
			throw new NotImplementedException();
		}

		public Task SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<User> GetWithAdministratedGroupsAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<User>> GetSuggestedAsync(string searchLetters)
		{
			throw new NotImplementedException();
		}

		public Task<List<User>> GetUsersByEmailsAsync(List<string> emails)
		{
			throw new NotImplementedException();
		}

		public IQueryable<User> GetSuggestedAsync(string searchLetters, Guid groupId)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetWithAdministratedAndModeratedGroups(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetWithAllGroupsAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetWithTeamsAsync(Guid userId)
		{
			throw new NotImplementedException();
		}
	}
}