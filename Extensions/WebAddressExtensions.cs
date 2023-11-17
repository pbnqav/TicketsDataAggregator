namespace TicketsDataAggregator.Extensions
{
    public static class WebAddressExtensions
    {
        public static string ExtractDomain(
            this string webAdress)
        {
            var lastDotIndex = webAdress.LastIndexOf('.');
            return webAdress.Substring(lastDotIndex);
        }
    }
}
