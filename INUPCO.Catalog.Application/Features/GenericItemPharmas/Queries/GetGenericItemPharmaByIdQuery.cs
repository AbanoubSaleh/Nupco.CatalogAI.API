using AutoMapper;
using INUPCO.Catalog.Application.DTOs;
using INUPCO.Catalog.Domain.Contracts;
using INUPCO.Catalog.Domain.Exceptions;
using MediatR;

namespace INUPCO.Catalog.Application.Features.GenericItemPharmas.Queries;

public record GetGenericItemPharmaByIdQuery(int Id) : IRequest<GenericItemPharmaDto>;

public class GetGenericItemPharmaByIdQueryHandler 
    : IRequestHandler<GetGenericItemPharmaByIdQuery, GenericItemPharmaDto>
{
    private readonly IGenericItemPharmaRepository _repository;
    private readonly IMapper _mapper;

    public GetGenericItemPharmaByIdQueryHandler(IGenericItemPharmaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GenericItemPharmaDto> Handle(GetGenericItemPharmaByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Generic Item with ID {request.Id} not found");

        return _mapper.Map<GenericItemPharmaDto>(entity);
    }
} 