using StudentOrganizer.Infrastructure.Commands;
using System;
using System.Text.Json.Serialization;

namespace StudentOrganizer.Infrastructure.Users.Commands
{
    public class DeleteUser : CreateCommandBase
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }

}