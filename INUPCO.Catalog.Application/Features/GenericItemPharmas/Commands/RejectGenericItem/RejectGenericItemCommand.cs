using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Exceptions;
using MediatR;

namespace INUPCO.Catalog.Application.Features.GenericItemPharmas.Commands.RejectGenericItem;

public record RejectGenericItemCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string RejectorId { get; init; } = string.Empty;
    public string Reason { get; init; } = string.Empty;
}

public class RejectGenericItemCommandHandler : IRequestHandler<RejectGenericItemCommand, Unit>
{
    private readonly IGenericItemPharmaRepository _repository;

    public RejectGenericItemCommandHandler(IGenericItemPharmaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(RejectGenericItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id) 
            ?? throw new NotFoundException($"Generic Item with ID {request.Id} not found");

        item.Reject(request.RejectorId, request.Reason);
        await _repository.UpdateAsync(item);

        return Unit.Value;
    }
} 