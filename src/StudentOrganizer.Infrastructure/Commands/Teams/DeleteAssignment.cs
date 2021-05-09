using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Teams
{
    public class DeleteAssignment
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public Guid GroupId { get; set; }
        [JsonIgnore]
        public string TeamName { get; set; }
        [JsonIgnore]
        public Guid AssignmentId { get; set; }
    }
}
