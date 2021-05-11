using System;
using Newtonsoft.Json;

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