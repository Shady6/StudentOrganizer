using AutoMapper;
using StudentOrganizer.Core.Models;
using StudentOrganizer.Core.Repositories;
using StudentOrganizer.Infrastructure.Dto;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;

        public AssignmentService(IAssignmentRepository assignmentRepository, IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssignmentDto>> BrowseAsync(string name = "")
        {
            var assignments = await _assignmentRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<AssignmentDto>>(assignments);
        }

        public async Task CreateAsync(Guid id, string name, string description, int semester, DateTime deadline)
        {
            var assignment = await _assignmentRepository.GetAsync(id);
            if(assignment != null)
            {
                throw new Exception("Such ID already exists");
            }
            assignment = new Assignment(name, description, semester, deadline);
            await _assignmentRepository.AddAsync(assignment);
        }

        public async Task<AssignmentDto> GetAsync(Guid id)
        {
            var assignment = await _assignmentRepository.GetAsync(id);
            if(assignment == null)
            {
                throw new Exception("There is no assignment with such id");
            }
            return _mapper.Map<AssignmentDto>(assignment);
        }
    }
}
