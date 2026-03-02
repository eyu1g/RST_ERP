using Lead.Api.Queries;
using Lead.Domain.DTO;
using MediatR;

<<<<<<< HEAD
namespace Lead.Api.Mediator;
=======
 namespace Lead.Api.Mediator;
>>>>>>> 49c0c04a3babf3157dc4f41253c3cf47d63e728a

public static class LookupsRequests
{
    public sealed record ListLeadStatusesQuery : IRequest<IReadOnlyList<LookupItemDto>>;

    public sealed class ListLeadStatusesQueryHandler : IRequestHandler<ListLeadStatusesQuery, IReadOnlyList<LookupItemDto>>
    {
        private readonly LookupQueries _queries;

        public ListLeadStatusesQueryHandler(LookupQueries queries)
        {
            _queries = queries;
        }

        public Task<IReadOnlyList<LookupItemDto>> Handle(ListLeadStatusesQuery request, CancellationToken cancellationToken) =>
            _queries.ListStatusesAsync(cancellationToken);
    }

    public sealed record ListLeadSourcesQuery : IRequest<IReadOnlyList<LookupItemDto>>;

    public sealed class ListLeadSourcesQueryHandler : IRequestHandler<ListLeadSourcesQuery, IReadOnlyList<LookupItemDto>>
    {
        private readonly LookupQueries _queries;

        public ListLeadSourcesQueryHandler(LookupQueries queries)
        {
            _queries = queries;
        }

        public Task<IReadOnlyList<LookupItemDto>> Handle(ListLeadSourcesQuery request, CancellationToken cancellationToken) =>
            _queries.ListSourcesAsync(cancellationToken);
    }

    public sealed record ListLeadStagesQuery : IRequest<IReadOnlyList<LookupItemDto>>;

    public sealed class ListLeadStagesQueryHandler : IRequestHandler<ListLeadStagesQuery, IReadOnlyList<LookupItemDto>>
    {
        private readonly LookupQueries _queries;

        public ListLeadStagesQueryHandler(LookupQueries queries)
        {
            _queries = queries;
        }

        public Task<IReadOnlyList<LookupItemDto>> Handle(ListLeadStagesQuery request, CancellationToken cancellationToken) =>
            _queries.ListStagesAsync(cancellationToken);
    }
}
