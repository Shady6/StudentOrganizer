using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Commands.Assignments
{
    public class CreateAssignment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Semester { get; set; }
        public DateTime Deadline { get; set; }
    }
}
