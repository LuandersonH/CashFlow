using PdfSharp.Charting;
using PdfSharp.Fonts;
using static PdfSharp.Snippets.Font.SegoeWpFontResolver;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;

public class ExpensesReportfontResolver : IFontResolver
{
    byte[]? IFontResolver.GetFont(string faceName)
    {
        throw new NotImplementedException();
    }

    FontResolverInfo? IFontResolver.ResolveTypeface(string familyName, bool bold, bool italic)
    {               
        return new FontResolverInfo(familyName);
    }
}
