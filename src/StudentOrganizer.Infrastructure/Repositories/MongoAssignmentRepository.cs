using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Repositories
{
    public class MongoAssignmentRepository : IAssignmentRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<Assignment> Assignments => _database.GetCollection<Assignment>("Assignments");

        public MongoAssignmentRepository(IConfiguration configuration)
        {
            var mongoSettings = new MongoSettings();
            configuration.GetSection("mongo").Bind(mongoSettings);
            mongoSettings.ConnectionString.Replace("<password>", configuration["MongoDbPassword"]);
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            _database = mongoClient.GetDatabase(mongoSettings.Database);
        }

        public async Task AddAsync(Assignment assignment)
            => await Assignments.InsertOneAsync(assignment);

        public async Task<IEnumerable<Assignment>> BrowseAsync(string name = "")
            => await Assignments.AsQueryable().ToListAsync();

        public async Task DeleteAsync(Guid id)
            => await Assignments.DeleteOneAsync(x => x.Id == id);

        public async Task<Assignment> GetAsync(Guid id)
            => await Assignments.AsQueryable().FirstOrDefaultAsync(x => id == x.Id);

        public async Task UpdateAsync(Assignment assignment)
            => await Assignments.ReplaceOneAsync(x => x.Id == assignment.Id, assignment);
    }
}
