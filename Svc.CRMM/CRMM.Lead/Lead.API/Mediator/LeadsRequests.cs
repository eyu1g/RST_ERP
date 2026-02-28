using Lead.Api.Command;
using Lead.Api.Queries;
using Lead.Domain.DTO;
using MediatR;

 namespace Lead.Api.Mediator;

public static class LeadsRequests
{
    public sealed record ListLeadsQuery : IRequest<IReadOnlyList<LeadDto>>;

    public sealed class ListLeadsQueryHandler : IRequestHandler<ListLeadsQuery, IReadOnlyList<LeadDto>>
    {
        private readonly LeadQueries _queries;

        public ListLeadsQueryHandler(LeadQueries queries)
        {
            _queries = queries;
        }

        public Task<IReadOnlyList<LeadDto>> Handle(ListLeadsQuery request, CancellationToken cancellationToken) =>
            _queries.ListAsync(cancellationToken);
    }

    public sealed record ListAssignedLeadsQuery : IRequest<IReadOnlyList<AssignedLeadRowDto>>;

    public sealed class ListAssignedLeadsQueryHandler : IRequestHandler<ListAssignedLeadsQuery, IReadOnlyList<AssignedLeadRowDto>>
    {
        private readonly LeadQueries _queries;

        public ListAssignedLeadsQueryHandler(LeadQueries queries)
        {
            _queries = queries;
        }

        public Task<IReadOnlyList<AssignedLeadRowDto>> Handle(ListAssignedLeadsQuery request, CancellationToken cancellationToken) =>
            _queries.ListAssignedAsync(cancellationToken);
    }

    public sealed record GetLeadQuery(Guid Id) : IRequest<LeadDto?>;

    public sealed class GetLeadQueryHandler : IRequestHandler<GetLeadQuery, LeadDto?>
    {
        private readonly LeadQueries _queries;

        public GetLeadQueryHandler(LeadQueries queries)
        {
            _queries = queries;
        }

        public Task<LeadDto?> Handle(GetLeadQuery request, CancellationToken cancellationToken) =>
            _queries.GetAsync(request.Id, cancellationToken);
    }

    public sealed record GetLeadDetailQuery(Guid Id) : IRequest<LeadDetailDto?>;

    public sealed class GetLeadDetailQueryHandler : IRequestHandler<GetLeadDetailQuery, LeadDetailDto?>
    {
        private readonly LeadQueries _queries;

        public GetLeadDetailQueryHandler(LeadQueries queries)
        {
            _queries = queries;
        }

        public Task<LeadDetailDto?> Handle(GetLeadDetailQuery request, CancellationToken cancellationToken) =>
            _queries.GetDetailAsync(request.Id, cancellationToken);
    }

    public sealed record CreateLeadRequestMessage(CreateLeadRequest Request) : IRequest<LeadDto>;

    public sealed class CreateLeadRequestHandler : IRequestHandler<CreateLeadRequestMessage, LeadDto>
    {
        private readonly CreateLeadCommand _command;

        public CreateLeadRequestHandler(CreateLeadCommand command)
        {
            _command = command;
        }

        public Task<LeadDto> Handle(CreateLeadRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Request, cancellationToken);
    }

    public sealed record UpdateLeadRequestMessage(Guid Id, UpdateLeadRequest Request) : IRequest<LeadDto?>;

    public sealed class UpdateLeadRequestHandler : IRequestHandler<UpdateLeadRequestMessage, LeadDto?>
    {
        private readonly UpdateLeadCommand _command;

        public UpdateLeadRequestHandler(UpdateLeadCommand command)
        {
            _command = command;
        }

        public Task<LeadDto?> Handle(UpdateLeadRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }

    public sealed record ChangeLeadStatusRequestMessage(Guid Id, ChangeLeadStatusRequest Request) : IRequest<LeadDto?>;

    public sealed class ChangeLeadStatusRequestHandler : IRequestHandler<ChangeLeadStatusRequestMessage, LeadDto?>
    {
        private readonly ChangeLeadStatusCommand _command;

        public ChangeLeadStatusRequestHandler(ChangeLeadStatusCommand command)
        {
            _command = command;
        }

        public Task<LeadDto?> Handle(ChangeLeadStatusRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }

    public sealed record AssignLeadRequestMessage(Guid Id, AssignLeadRequest Request) : IRequest<LeadDto?>;

    public sealed class AssignLeadRequestHandler : IRequestHandler<AssignLeadRequestMessage, LeadDto?>
    {
        private readonly AssignLeadCommand _command;

        public AssignLeadRequestHandler(AssignLeadCommand command)
        {
            _command = command;
        }

        public Task<LeadDto?> Handle(AssignLeadRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }

    public sealed record ConvertLeadRequestMessage(Guid Id, ConvertLeadRequest Request) : IRequest<ConvertLeadResultDto?>;

    public sealed class ConvertLeadRequestHandler : IRequestHandler<ConvertLeadRequestMessage, ConvertLeadResultDto?>
    {
        private readonly ConvertLeadCommand _command;

        public ConvertLeadRequestHandler(ConvertLeadCommand command)
        {
            _command = command;
        }

        public Task<ConvertLeadResultDto?> Handle(ConvertLeadRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }

    public sealed record AddLeadScoreRequestMessage(Guid Id, UpdateLeadScoreRequest Request) : IRequest<LeadScoreHistoryDto?>;

    public sealed class AddLeadScoreRequestHandler : IRequestHandler<AddLeadScoreRequestMessage, LeadScoreHistoryDto?>
    {
        private readonly AddLeadScoreCommand _command;

        public AddLeadScoreRequestHandler(AddLeadScoreCommand command)
        {
            _command = command;
        }

        public Task<LeadScoreHistoryDto?> Handle(AddLeadScoreRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, request.Request, cancellationToken);
    }

    public sealed record DeleteLeadRequestMessage(Guid Id) : IRequest<bool>;

    public sealed class DeleteLeadRequestHandler : IRequestHandler<DeleteLeadRequestMessage, bool>
    {
        private readonly DeleteLeadCommand _command;

        public DeleteLeadRequestHandler(DeleteLeadCommand command)
        {
            _command = command;
        }

        public Task<bool> Handle(DeleteLeadRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Id, cancellationToken);
    }

    public sealed record ImportLeadsRequestMessage(Stream Stream) : IRequest<ImportLeadsResultDto>;

    public sealed class ImportLeadsRequestHandler : IRequestHandler<ImportLeadsRequestMessage, ImportLeadsResultDto>
    {
        private readonly ImportLeadsCommand _command;

        public ImportLeadsRequestHandler(ImportLeadsCommand command)
        {
            _command = command;
        }

        public Task<ImportLeadsResultDto> Handle(ImportLeadsRequestMessage request, CancellationToken cancellationToken) =>
            _command.ExecuteAsync(request.Stream, cancellationToken);
    }
}
