using MediatR;
using StudentOrganizer.Infrastructure.Commands.Groups;
using StudentOrganizer.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentOrganizer.Infrastructure.Handlers.Groups
{
    public class CreateGroupHandler : IRequestHandler<CreateGroup>
    {
        private readonly IGroupService groupService;

        public CreateGroupHandler(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public async Task<Unit> Handle(CreateGroup command, CancellationToken cancellationToken)
        {
            await groupService.CreateAsync(command);
            return Unit.Value;
        }
    }
}
