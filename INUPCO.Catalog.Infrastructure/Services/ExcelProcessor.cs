using ClosedXML.Excel;
using INUPCO.Catalog.Application.Common.Interfaces;

namespace INUPCO.Catalog.Infrastructure.Services;

public class ExcelProcessor : IExcelProcessor
{
    public async Task<List<CustomerMappingDto>> ProcessMappingsFromExcel(Stream fileStream)
    {
        var mappings = new List<CustomerMappingDto>();
        using var workbook = new XLWorkbook(fileStream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RowsUsed().Skip(1); // Skip header row

        foreach (var row in rows)
        {
            if (row.IsEmpty()) continue;

            mappings.Add(new CustomerMappingDto
            {
                CustomerCode = row.Cell(1).GetString().Trim(),
                CustomerSpecificCode = row.Cell(2).GetString().Trim(),
                GenericItemCode = row.Cell(3).GetString().Trim()
            });
        }

        return mappings;
    }

    public async Task<byte[]> GenerateTemplate(IEnumerable<string> genericCodes)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Mapping Template");

        // Add headers with styling
        worksheet.Cell(1, 1).Value = "Customer Code";
        worksheet.Cell(1, 2).Value = "Customer Specific Code";
        worksheet.Cell(1, 3).Value = "NUPCO Generic Code";

        // Style header row
        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        // Add data validation for generic codes column
        var validationRange = worksheet.Range(2, 3, 1000, 3);
        var validation = validationRange.SetDataValidation();
        validation.List(genericCodes.FirstOrDefault(), true);
        
        // Add instructions in cell comments
        worksheet.Cell(1, 1).CreateComment().AddText("Enter your organization's customer code");
        worksheet.Cell(1, 2).CreateComment().AddText("Enter your internal product code");
        worksheet.Cell(1, 3).CreateComment().AddText("Select NUPCO generic code from the dropdown");

        // Format columns
        worksheet.Columns().AdjustToContents();
        worksheet.Column(1).Width = 15;
        worksheet.Column(2).Width = 20;
        worksheet.Column(3).Width = 25;

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return stream.ToArray();
    }

    public byte[] GenerateErrorReport(List<string> errors)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Error Report");

        // Add headers with styling
        worksheet.Cell(1, 1).Value = "Row Number";
        worksheet.Cell(1, 2).Value = "Error Description";

        // Style header row
        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        // Add error data
        for (int i = 0; i < errors.Count; i++)
        {
            var row = i + 2;
            worksheet.Cell(row, 1).Value = i + 1;
            worksheet.Cell(row, 2).Value = errors[i];
            
            // Style error rows
            var errorRow = worksheet.Row(row);
            errorRow.Style.Fill.BackgroundColor = XLColor.LightPink;
        }

        // Format columns
        worksheet.Column(1).Width = 12;
        worksheet.Column(2).Width = 80;
        worksheet.Column(2).Style.Alignment.WrapText = true;

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return stream.ToArray();
    }

    public async Task<List<SubsidiaryImportDto>> ProcessSubsidiariesFromExcel(Stream fileStream)
    {
        var subsidiaries = new List<SubsidiaryImportDto>();
        using var workbook = new XLWorkbook(fileStream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RowsUsed().Skip(1); // Skip header row

        foreach (var row in rows)
        {
            if (row.IsEmpty()) continue;

            subsidiaries.Add(new SubsidiaryImportDto
            {
                Name = row.Cell(1).GetString().Trim(),
                Country = row.Cell(2).GetString().Trim(),
                ManufacturerName = row.Cell(3).GetString().Trim(),
                ManufacturerCountry = row.Cell(4).GetString().Trim()
            });
        }

        return subsidiaries;
    }

    public async Task<List<ManufacturerImportDto>> ProcessManufacturersFromExcel(Stream fileStream)
    {
        var manufacturers = new List<ManufacturerImportDto>();
        using var workbook = new XLWorkbook(fileStream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RowsUsed().Skip(1); // Skip header row

        foreach (var row in rows)
        {
            if (row.IsEmpty()) continue;

            manufacturers.Add(new ManufacturerImportDto
            {
                TradeCode = row.Cell(1).GetString().Trim(),
                Name = row.Cell(2).GetString().Trim(),
                Country = row.Cell(3).GetString().Trim()
            });
        }

        return manufacturers;
    }

    public byte[] GenerateManufacturerTemplate()
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Product Trade Code Update");

        // Add headers
        worksheet.Cell(1, 1).Value = "Trade Code";
        worksheet.Cell(1, 2).Value = "Manufacturer/Subsidiary Name";
        worksheet.Cell(1, 3).Value = "Country";

        // Format headers
        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

        // Add sample data
        worksheet.Cell(2, 1).Value = "PFE001";
        worksheet.Cell(2, 2).Value = "Pfizer Germany GmbH";
        worksheet.Cell(2, 3).Value = "Germany";

        // Adjust column widths
        worksheet.Column(1).Width = 15;
        worksheet.Column(2).Width = 35;
        worksheet.Column(3).Width = 20;

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
} 