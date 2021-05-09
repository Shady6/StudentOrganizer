using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Settings;

namespace StudentOrganizer.Infrastructure.Mongo.Repositories
{
	public class MongoGroupRepository : IGroupRepository
	{
		private readonly IMongoDatabase _database;
		private IMongoCollection<Group> Groups => _database.GetCollection<Group>("Groups");

		public MongoGroupRepository(IConfiguration configuration)
		{
			var mongoSettings = new MongoSettings();
			configuration.GetSection("mongo").Bind(mongoSettings);
			mongoSettings.ConnectionString =
				mongoSettings.ConnectionString.Replace("<password>", configuration["MongoDbPassword"]);
			var mongoClient = new MongoClient(mongoSettings.ConnectionString);
			_database = mongoClient.GetDatabase(mongoSettings.Database);
		}

		public async Task AddAsync(Group group)
		{
			await Groups.InsertOneAsync(group);
		}

		public async Task DeleteAsync(Guid id)
		{
			await Groups.DeleteOneAsync(u => u.Id == id);
		}

		public async Task<Group> GetAsync(Guid id)
		{
			return await Groups.AsQueryable().FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<Group> GetAsync(string name)
		{
			return await Groups.AsQueryable().FirstOrDefaultAsync(u => u.Name == name);
		}

		public async Task UpdateAsync(Group group)
		{
			await Groups.ReplaceOneAsync(u => u.Id == group.Id, group);
		}

		public IQueryable<Group> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetWholeGroupAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(Group group)
		{
			throw new NotImplementedException();
		}

		public Task SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetWithCoursesAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetWithTeamsAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetWithTeamScheduleAndCourses(Guid id, string teamName)
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetWithStudents(Guid GroupId)
		{
			throw new NotImplementedException();
		}

        public Task<Group> GetWithGroupAndTeamStudents(Guid id)
        {
            throw new NotImplementedException();
        }
    
		public IQueryable<Group> GetWholeGroupsAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetWithAllUsers(Guid groupId)
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetWithTeamsAndStudentsAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Group> GetWithTeamStudentsAsync(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}