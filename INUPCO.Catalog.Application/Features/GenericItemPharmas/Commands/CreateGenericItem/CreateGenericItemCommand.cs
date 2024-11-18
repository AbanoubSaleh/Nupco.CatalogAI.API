using AutoMapper;
using INUPCO.Catalog.Application.DTOs;
using INUPCO.Catalog.Domain.Contracts;
using MediatR;

namespace INUPCO.Catalog.Application.Features.GenericItemPharmas.Commands.CreateGenericItem;

public record CreateGenericItemCommand : IRequest<GenericItemPharmaDto>
{
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? CustomerCode { get; init; }
}

public class CreateGenericItemCommandHandler : IRequestHandler<CreateGenericItemCommand, GenericItemPharmaDto>
{
    private readonly IGenericItemPharmaRepository _repository;
    private readonly IMapper _mapper;

    public CreateGenericItemCommandHandler(IGenericItemPharmaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GenericItemPharmaDto> Handle(CreateGenericItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.CreateAsync(new Domain.DTOs.GenericItemPharmaCreateDto
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            CustomerCode = request.CustomerCode,
            IsActive = true
        });

        return _mapper.Map<GenericItemPharmaDto>(entity);
    }
} 