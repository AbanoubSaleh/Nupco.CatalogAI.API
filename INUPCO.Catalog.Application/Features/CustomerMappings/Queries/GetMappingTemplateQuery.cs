using INUPCO.Catalog.Application.Common.Interfaces;
using INUPCO.Catalog.Domain.Contracts;
using MediatR;

namespace INUPCO.Catalog.Application.Features.CustomerMappings.Queries;

public record GetMappingTemplateQuery : IRequest<byte[]>;

public class GetMappingTemplateQueryHandler : IRequestHandler<GetMappingTemplateQuery, byte[]>
{
    private readonly IExcelProcessor _excelProcessor;
    private readonly IGenericItemPharmaRepository _genericItemRepository;

    public GetMappingTemplateQueryHandler(
        IExcelProcessor excelProcessor,
        IGenericItemPharmaRepository genericItemRepository)
    {
        _excelProcessor = excelProcessor;
        _genericItemRepository = genericItemRepository;
    }

    public async Task<byte[]> Handle(GetMappingTemplateQuery request, CancellationToken cancellationToken)
    {
        var codes = await _genericItemRepository.GetAllAsync();
        return await _excelProcessor.GenerateTemplate(codes);
    }
} 