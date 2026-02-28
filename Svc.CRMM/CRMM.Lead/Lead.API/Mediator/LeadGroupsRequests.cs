using Lead.Api.Command;
using Lead.Api.Queries;
using Lead.App.Service;
using Lead.Domain.DTO;
using MediatR;

namespace Lead.Api.Mediator;

public static class LeadGroupsRequests
{
    public sealed record GetLeadGroupSettingsQuery : IRequest<LeadGroupSettingsDto>;

    public sealed class GetLeadGroupSettingsQueryHandler : IRequestHandler<GetLeadGroupSettingsQuery, LeadGroupSettingsDto>
    {
        private readonly LeadGroupSettingsService _settings;

        public GetLeadGroupSettingsQueryHandler(LeadGroupSettingsService settings)
        {
            _settings = settings;
        }

        public Task<LeadGroupSettingsDto> Handle(GetLeadGroupSettingsQuery request, CancellationToken cancellationToken) =>
            Task.FromResult(_settings.GetSettings());
    }

    public sealed record ListLeadGroupsQuery : IRequest<IReadOnlyList<LeadGroupDto>>;

    public sealed class ListLeadGroupsQueryHandler : IRequestHandler<ListLeadGroupsQuery, IReadOnlyList<LeadGroupDto>>
    {
        private readonly LeadGroupQueries _queries;

        public ListLeadGroupsQueryHandler(LeadGroupQueries queries)
        {
            _queries = queries;
        }

        public Task<IReadOnlyList<LeadGroupDto>> Handle(ListLeadGroupsQuery request, CancellationToken cancellationToken) =>
            _queries.ListAsync(cancellationToken);
    }

    public sealed record GetLeadGroupQuery(Guid Id) : IRequest<(LeadGroupDto Group, IReadOnlyList<LeadGroupConditionDto> Conditions)?>;

    public sealed class GetLeadGroupQueryHandler : IRequestHandler<GetLeadGroupQuery, (LeadGroupDto Group, IReadOnlyList<LeadGroupConditionDto> Conditions)?>
    {
        private readonly LeadGroupQueries _queries;

        public GetLeadGroupQueryHandler(LeadGroupQueries queries)
        {
            _queries = queries;
        }

        public Task<(LeadGroupDto Group, IReadOnlyList<LeadGroupConditionDto> Conditions)?> Handle(GetLeadGroupQuery request, CancellationToken cancellationToken) =>
            _queries.GetAsync(request.Id, cancellationToken);
    }

    public sealed record CreateLeadGroupRequestMessage(CreateLeadGroupRequest Request) : IRequest<LeadGroupDto>;

    public sealed class CreateLeadGroupRequestHandler : IRequestHandler<CreateLeadGroupRequestMessage, LeadGroupDto>
    {
        private readonly CreateLeadGroupCommand _command;

        public CreateLeadGroupRequestHandler(CreateLeadGroupCommand command)
        {
            _command = command;
        }

        public Task<LeadGroupDto> Handle(CreateLeadGroupRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Request, cancellationToken);
    }

    public sealed record UpdateLeadGroupRequestMessage(Guid Id, UpdateLeadGroupRequest Request) : IRequest<LeadGroupDto?>;

    public sealed class UpdateLeadGroupRequestHandler : IRequestHandler<UpdateLeadGroupRequestMessage, LeadGroupDto?>
    {
        private readonly UpdateLeadGroupCommand _command;

        public UpdateLeadGroupRequestHandler(UpdateLeadGroupCommand command)
        {
            _command = command;
        }

        public Task<LeadGroupDto?> Handle(UpdateLeadGroupRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }

    public sealed record DeleteLeadGroupRequestMessage(Guid Id) : IRequest<bool>;

    public sealed class DeleteLeadGroupRequestHandler : IRequestHandler<DeleteLeadGroupRequestMessage, bool>
    {
        private readonly DeleteLeadGroupCommand _command;

        public DeleteLeadGroupRequestHandler(DeleteLeadGroupCommand command)
        {
            _command = command;
        }

        public Task<bool> Handle(DeleteLeadGroupRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, cancellationToken);
    }

    public sealed record ListLeadGroupConditionsQuery(Guid LeadGroupId) : IRequest<IReadOnlyList<LeadGroupConditionDto>>;

    public sealed class ListLeadGroupConditionsQueryHandler : IRequestHandler<ListLeadGroupConditionsQuery, IReadOnlyList<LeadGroupConditionDto>>
    {
        private readonly LeadGroupQueries _queries;

        public ListLeadGroupConditionsQueryHandler(LeadGroupQueries queries)
        {
            _queries = queries;
        }

        public Task<IReadOnlyList<LeadGroupConditionDto>> Handle(ListLeadGroupConditionsQuery request, CancellationToken cancellationToken) =>
            _queries.ListConditionsAsync(request.LeadGroupId, cancellationToken);
    }

    public sealed record CreateLeadGroupConditionRequestMessage(Guid LeadGroupId, CreateLeadGroupConditionRequest Request) : IRequest<LeadGroupConditionDto>;

    public sealed class CreateLeadGroupConditionRequestHandler : IRequestHandler<CreateLeadGroupConditionRequestMessage, LeadGroupConditionDto>
    {
        private readonly CreateLeadGroupConditionCommand _command;

        public CreateLeadGroupConditionRequestHandler(CreateLeadGroupConditionCommand command)
        {
            _command = command;
        }

        public Task<LeadGroupConditionDto> Handle(CreateLeadGroupConditionRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.LeadGroupId, request.Request, cancellationToken);
    }

    public sealed record UpdateLeadGroupConditionRequestMessage(Guid Id, UpdateLeadGroupConditionRequest Request) : IRequest<LeadGroupConditionDto?>;

    public sealed class UpdateLeadGroupConditionRequestHandler : IRequestHandler<UpdateLeadGroupConditionRequestMessage, LeadGroupConditionDto?>
    {
        private readonly UpdateLeadGroupConditionCommand _command;

        public UpdateLeadGroupConditionRequestHandler(UpdateLeadGroupConditionCommand command)
        {
            _command = command;
        }

        public Task<LeadGroupConditionDto?> Handle(UpdateLeadGroupConditionRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }

    public sealed record DeleteLeadGroupConditionRequestMessage(Guid Id) : IRequest<bool>;

    public sealed class DeleteLeadGroupConditionRequestHandler : IRequestHandler<DeleteLeadGroupConditionRequestMessage, bool>
    {
        private readonly DeleteLeadGroupConditionCommand _command;

        public DeleteLeadGroupConditionRequestHandler(DeleteLeadGroupConditionCommand command)
        {
            _command = command;
        }

        public Task<bool> Handle(DeleteLeadGroupConditionRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, cancellationToken);
    }
}
