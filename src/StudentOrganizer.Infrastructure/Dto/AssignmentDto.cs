using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Dto
{
    public class AssignmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
