using Lead.Api.Command;
using Lead.Api.Queries;
using Lead.Domain.DTO;
using MediatR;

namespace Lead.Api.Mediator;

public static class AdminRequests
{
    public static class LeadStatuses
    {
        public sealed record ListQuery : IRequest<IReadOnlyList<LeadStatusAdminDto>>;
        public sealed class ListHandler : IRequestHandler<ListQuery, IReadOnlyList<LeadStatusAdminDto>>
        {
            private readonly LeadStatusAdminQueries _queries;
            public ListHandler(LeadStatusAdminQueries queries) { _queries = queries; }
            public Task<IReadOnlyList<LeadStatusAdminDto>> Handle(ListQuery request, CancellationToken cancellationToken) =>
                _queries.ListAsync(cancellationToken);
        }

        public sealed record GetQuery(Guid Id) : IRequest<LeadStatusAdminDto?>;
        public sealed class GetHandler : IRequestHandler<GetQuery, LeadStatusAdminDto?>
        {
            private readonly LeadStatusAdminQueries _queries;
            public GetHandler(LeadStatusAdminQueries queries) { _queries = queries; }
            public Task<LeadStatusAdminDto?> Handle(GetQuery request, CancellationToken cancellationToken) =>
                _queries.GetAsync(request.Id, cancellationToken);
        }

        public sealed record CreateCommand(CreateLeadStatusRequest Request) : IRequest<LeadStatusAdminDto>;
        public sealed class CreateHandler : IRequestHandler<CreateCommand, LeadStatusAdminDto>
        {
            private readonly CreateLeadStatusAdminCommand _command;
            public CreateHandler(CreateLeadStatusAdminCommand command) { _command = command; }
            public Task<LeadStatusAdminDto> Handle(CreateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Request, cancellationToken);
        }

        public sealed record UpdateCommand(Guid Id, UpdateLeadStatusRequest Request) : IRequest<LeadStatusAdminDto?>;
        public sealed class UpdateHandler : IRequestHandler<UpdateCommand, LeadStatusAdminDto?>
        {
            private readonly UpdateLeadStatusAdminCommand _command;
            public UpdateHandler(UpdateLeadStatusAdminCommand command) { _command = command; }
            public Task<LeadStatusAdminDto?> Handle(UpdateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
        }

        public sealed record SetActiveCommand(Guid Id, bool IsActive) : IRequest<LeadStatusAdminDto?>;
        public sealed class SetActiveHandler : IRequestHandler<SetActiveCommand, LeadStatusAdminDto?>
        {
            private readonly SetLeadStatusActiveAdminCommand _command;
            public SetActiveHandler(SetLeadStatusActiveAdminCommand command) { _command = command; }
            public Task<LeadStatusAdminDto?> Handle(SetActiveCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.IsActive, cancellationToken);
        }

        public sealed record DeleteCommand(Guid Id) : IRequest<bool>;
        public sealed class DeleteHandler : IRequestHandler<DeleteCommand, bool>
        {
            private readonly DeleteLeadStatusAdminCommand _command;
            public DeleteHandler(DeleteLeadStatusAdminCommand command) { _command = command; }
            public Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, cancellationToken);
        }
    }

    public static class LeadSources
    {
        public sealed record ListQuery : IRequest<IReadOnlyList<LeadSourceAdminDto>>;
        public sealed class ListHandler : IRequestHandler<ListQuery, IReadOnlyList<LeadSourceAdminDto>>
        {
            private readonly LeadSourceAdminQueries _queries;
            public ListHandler(LeadSourceAdminQueries queries) { _queries = queries; }
            public Task<IReadOnlyList<LeadSourceAdminDto>> Handle(ListQuery request, CancellationToken cancellationToken) =>
                _queries.ListAsync(cancellationToken);
        }

        public sealed record GetQuery(Guid Id) : IRequest<LeadSourceAdminDto?>;
        public sealed class GetHandler : IRequestHandler<GetQuery, LeadSourceAdminDto?>
        {
            private readonly LeadSourceAdminQueries _queries;
            public GetHandler(LeadSourceAdminQueries queries) { _queries = queries; }
            public Task<LeadSourceAdminDto?> Handle(GetQuery request, CancellationToken cancellationToken) =>
                _queries.GetAsync(request.Id, cancellationToken);
        }

        public sealed record CreateCommand(CreateLeadSourceRequest Request) : IRequest<LeadSourceAdminDto>;
        public sealed class CreateHandler : IRequestHandler<CreateCommand, LeadSourceAdminDto>
        {
            private readonly CreateLeadSourceAdminCommand _command;
            public CreateHandler(CreateLeadSourceAdminCommand command) { _command = command; }
            public Task<LeadSourceAdminDto> Handle(CreateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Request, cancellationToken);
        }

        public sealed record UpdateCommand(Guid Id, UpdateLeadSourceRequest Request) : IRequest<LeadSourceAdminDto?>;
        public sealed class UpdateHandler : IRequestHandler<UpdateCommand, LeadSourceAdminDto?>
        {
            private readonly UpdateLeadSourceAdminCommand _command;
            public UpdateHandler(UpdateLeadSourceAdminCommand command) { _command = command; }
            public Task<LeadSourceAdminDto?> Handle(UpdateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
        }

        public sealed record SetActiveCommand(Guid Id, bool IsActive) : IRequest<LeadSourceAdminDto?>;
        public sealed class SetActiveHandler : IRequestHandler<SetActiveCommand, LeadSourceAdminDto?>
        {
            private readonly SetLeadSourceActiveAdminCommand _command;
            public SetActiveHandler(SetLeadSourceActiveAdminCommand command) { _command = command; }
            public Task<LeadSourceAdminDto?> Handle(SetActiveCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.IsActive, cancellationToken);
        }

        public sealed record DeleteCommand(Guid Id) : IRequest<bool>;
        public sealed class DeleteHandler : IRequestHandler<DeleteCommand, bool>
        {
            private readonly DeleteLeadSourceAdminCommand _command;
            public DeleteHandler(DeleteLeadSourceAdminCommand command) { _command = command; }
            public Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, cancellationToken);
        }
    }

    public static class LeadIndustries
    {
        public sealed record ListQuery : IRequest<IReadOnlyList<LeadIndustryAdminDto>>;
        public sealed class ListHandler : IRequestHandler<ListQuery, IReadOnlyList<LeadIndustryAdminDto>>
        {
            private readonly LeadIndustryAdminQueries _queries;
            public ListHandler(LeadIndustryAdminQueries queries) { _queries = queries; }
            public Task<IReadOnlyList<LeadIndustryAdminDto>> Handle(ListQuery request, CancellationToken cancellationToken) =>
                _queries.ListAsync(cancellationToken);
        }

        public sealed record GetQuery(Guid Id) : IRequest<LeadIndustryAdminDto?>;
        public sealed class GetHandler : IRequestHandler<GetQuery, LeadIndustryAdminDto?>
        {
            private readonly LeadIndustryAdminQueries _queries;
            public GetHandler(LeadIndustryAdminQueries queries) { _queries = queries; }
            public Task<LeadIndustryAdminDto?> Handle(GetQuery request, CancellationToken cancellationToken) =>
                _queries.GetAsync(request.Id, cancellationToken);
        }

        public sealed record CreateCommand(CreateLeadIndustryRequest Request) : IRequest<LeadIndustryAdminDto>;
        public sealed class CreateHandler : IRequestHandler<CreateCommand, LeadIndustryAdminDto>
        {
            private readonly CreateLeadIndustryAdminCommand _command;
            public CreateHandler(CreateLeadIndustryAdminCommand command) { _command = command; }
            public Task<LeadIndustryAdminDto> Handle(CreateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Request, cancellationToken);
        }

        public sealed record UpdateCommand(Guid Id, UpdateLeadIndustryRequest Request) : IRequest<LeadIndustryAdminDto?>;
        public sealed class UpdateHandler : IRequestHandler<UpdateCommand, LeadIndustryAdminDto?>
        {
            private readonly UpdateLeadIndustryAdminCommand _command;
            public UpdateHandler(UpdateLeadIndustryAdminCommand command) { _command = command; }
            public Task<LeadIndustryAdminDto?> Handle(UpdateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
        }

        public sealed record SetActiveCommand(Guid Id, bool IsActive) : IRequest<LeadIndustryAdminDto?>;
        public sealed class SetActiveHandler : IRequestHandler<SetActiveCommand, LeadIndustryAdminDto?>
        {
            private readonly SetLeadIndustryActiveAdminCommand _command;
            public SetActiveHandler(SetLeadIndustryActiveAdminCommand command) { _command = command; }
            public Task<LeadIndustryAdminDto?> Handle(SetActiveCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.IsActive, cancellationToken);
        }

        public sealed record DeleteCommand(Guid Id) : IRequest<bool>;
        public sealed class DeleteHandler : IRequestHandler<DeleteCommand, bool>
        {
            private readonly DeleteLeadIndustryAdminCommand _command;
            public DeleteHandler(DeleteLeadIndustryAdminCommand command) { _command = command; }
            public Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, cancellationToken);
        }
    }

    public static class LeadScoringRules
    {
        public sealed record ListQuery : IRequest<IReadOnlyList<LeadScoringRuleDto>>;
        public sealed class ListHandler : IRequestHandler<ListQuery, IReadOnlyList<LeadScoringRuleDto>>
        {
            private readonly LeadScoringRuleQueries _queries;
            public ListHandler(LeadScoringRuleQueries queries) { _queries = queries; }
            public Task<IReadOnlyList<LeadScoringRuleDto>> Handle(ListQuery request, CancellationToken cancellationToken) =>
                _queries.ListAsync(cancellationToken);
        }

        public sealed record GetQuery(Guid Id) : IRequest<LeadScoringRuleDto?>;
        public sealed class GetHandler : IRequestHandler<GetQuery, LeadScoringRuleDto?>
        {
            private readonly LeadScoringRuleQueries _queries;
            public GetHandler(LeadScoringRuleQueries queries) { _queries = queries; }
            public Task<LeadScoringRuleDto?> Handle(GetQuery request, CancellationToken cancellationToken) =>
                _queries.GetAsync(request.Id, cancellationToken);
        }

        public sealed record CreateCommand(CreateLeadScoringRuleRequest Request) : IRequest<LeadScoringRuleDto>;
        public sealed class CreateHandler : IRequestHandler<CreateCommand, LeadScoringRuleDto>
        {
            private readonly CreateLeadScoringRuleCommand _command;
            public CreateHandler(CreateLeadScoringRuleCommand command) { _command = command; }
            public Task<LeadScoringRuleDto> Handle(CreateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Request, cancellationToken);
        }

        public sealed record UpdateCommand(Guid Id, UpdateLeadScoringRuleRequest Request) : IRequest<LeadScoringRuleDto?>;
        public sealed class UpdateHandler : IRequestHandler<UpdateCommand, LeadScoringRuleDto?>
        {
            private readonly UpdateLeadScoringRuleCommand _command;
            public UpdateHandler(UpdateLeadScoringRuleCommand command) { _command = command; }
            public Task<LeadScoringRuleDto?> Handle(UpdateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
        }

        public sealed record SetActiveCommand(Guid Id, bool IsActive) : IRequest<LeadScoringRuleDto?>;
        public sealed class SetActiveHandler : IRequestHandler<SetActiveCommand, LeadScoringRuleDto?>
        {
            private readonly SetLeadScoringRuleActiveCommand _command;
            public SetActiveHandler(SetLeadScoringRuleActiveCommand command) { _command = command; }
            public Task<LeadScoringRuleDto?> Handle(SetActiveCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.IsActive, cancellationToken);
        }

        public sealed record DeleteCommand(Guid Id) : IRequest<bool>;
        public sealed class DeleteHandler : IRequestHandler<DeleteCommand, bool>
        {
            private readonly DeleteLeadScoringRuleCommand _command;
            public DeleteHandler(DeleteLeadScoringRuleCommand command) { _command = command; }
            public Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, cancellationToken);
        }
    }

    public static class LeadRoutingRules
    {
        public sealed record ListQuery : IRequest<IReadOnlyList<LeadRoutingRuleDto>>;
        public sealed class ListHandler : IRequestHandler<ListQuery, IReadOnlyList<LeadRoutingRuleDto>>
        {
            private readonly LeadRoutingRuleQueries _queries;
            public ListHandler(LeadRoutingRuleQueries queries) { _queries = queries; }
            public Task<IReadOnlyList<LeadRoutingRuleDto>> Handle(ListQuery request, CancellationToken cancellationToken) =>
                _queries.ListAsync(cancellationToken);
        }

        public sealed record GetQuery(Guid Id) : IRequest<LeadRoutingRuleDto?>;
        public sealed class GetHandler : IRequestHandler<GetQuery, LeadRoutingRuleDto?>
        {
            private readonly LeadRoutingRuleQueries _queries;
            public GetHandler(LeadRoutingRuleQueries queries) { _queries = queries; }
            public Task<LeadRoutingRuleDto?> Handle(GetQuery request, CancellationToken cancellationToken) =>
                _queries.GetAsync(request.Id, cancellationToken);
        }

        public sealed record CreateCommand(CreateLeadRoutingRuleRequest Request) : IRequest<LeadRoutingRuleDto>;
        public sealed class CreateHandler : IRequestHandler<CreateCommand, LeadRoutingRuleDto>
        {
            private readonly CreateLeadRoutingRuleCommand _command;
            public CreateHandler(CreateLeadRoutingRuleCommand command) { _command = command; }
            public Task<LeadRoutingRuleDto> Handle(CreateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Request, cancellationToken);
        }

        public sealed record UpdateCommand(Guid Id, UpdateLeadRoutingRuleRequest Request) : IRequest<LeadRoutingRuleDto?>;
        public sealed class UpdateHandler : IRequestHandler<UpdateCommand, LeadRoutingRuleDto?>
        {
            private readonly UpdateLeadRoutingRuleCommand _command;
            public UpdateHandler(UpdateLeadRoutingRuleCommand command) { _command = command; }
            public Task<LeadRoutingRuleDto?> Handle(UpdateCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
        }

        public sealed record SetActiveCommand(Guid Id, bool IsActive) : IRequest<LeadRoutingRuleDto?>;
        public sealed class SetActiveHandler : IRequestHandler<SetActiveCommand, LeadRoutingRuleDto?>
        {
            private readonly SetLeadRoutingRuleActiveCommand _command;
            public SetActiveHandler(SetLeadRoutingRuleActiveCommand command) { _command = command; }
            public Task<LeadRoutingRuleDto?> Handle(SetActiveCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, request.IsActive, cancellationToken);
        }

        public sealed record DeleteCommand(Guid Id) : IRequest<bool>;
        public sealed class DeleteHandler : IRequestHandler<DeleteCommand, bool>
        {
            private readonly DeleteLeadRoutingRuleCommand _command;
            public DeleteHandler(DeleteLeadRoutingRuleCommand command) { _command = command; }
            public Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken) =>
                _command.ExecuteAsync(request.Id, cancellationToken);
        }
    }
}
