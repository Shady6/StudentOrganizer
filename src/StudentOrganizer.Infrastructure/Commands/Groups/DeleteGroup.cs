using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Commands.Groups
{
    public class DeleteGroup : CreateCommandBase
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public Guid GroupId { get; set; }
    }
}