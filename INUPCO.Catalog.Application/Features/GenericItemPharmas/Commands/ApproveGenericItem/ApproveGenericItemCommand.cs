using INUPCO.Catalog.Domain.Contracts;
using MediatR;

namespace INUPCO.Catalog.Application.Features.GenericItemPharmas.Commands.ApproveGenericItem;

public record ApproveGenericItemCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string ApproverId { get; init; } = string.Empty;
}

public class ApproveGenericItemCommandHandler : IRequestHandler<ApproveGenericItemCommand, Unit>
{
    private readonly IGenericItemPharmaRepository _repository;

    public ApproveGenericItemCommandHandler(IGenericItemPharmaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(ApproveGenericItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id) 
            ?? throw new DirectoryNotFoundException($"Generic Item with ID {request.Id} not found");

        item.Approve(request.ApproverId);
        await _repository.UpdateAsync(item);

        return Unit.Value;
    }
} 