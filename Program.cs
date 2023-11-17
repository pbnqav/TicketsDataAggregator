using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using System.Globalization;
using System.Text;

namespace TicketsDataAggregator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string TicketsFolder = @"c:\ourcinema\tickets\";

            try
            {
                var ticketsAggregator = new TicketsAggregator(
                    TicketsFolder);

                ticketsAggregator.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured."+
                    "Exception message: " + ex.Message);
            }
         }
    }

    public class TicketsAggregator
    {
        private string _ticketsFolder;
        private readonly Dictionary<string, string> _domainCulture = new()
        {
            [".com"] = "en-US",
            [".fr"] = "fr-FR",
            [".jp"] = "jp-JP"
        };

        public TicketsAggregator(string ticketsFolder)
        {
            _ticketsFolder = ticketsFolder;
        }

        public void Run()
        {
            // vamos utilizar uma StringBuilder apenas para treino
            // ir gravando para um ficheiro o resultado seria melhor
            // até em termos de memória
            var stringBuilder = new StringBuilder();

            foreach (var filePath in Directory.GetFiles(
                _ticketsFolder, "*.pdf"))
            {
                using PdfDocument document = PdfDocument.Open(filePath);
                // Page number starts from 1, not 0.
                Page page = document.GetPage(1);
                ProcessPage(stringBuilder, page);
            }
            var resultPath = Path.Combine(
                    _ticketsFolder, "aggregatedTickets.txt");
            File.WriteAllText(resultPath, stringBuilder.ToString());
            Console.WriteLine("Results saved to " + resultPath);
        }

        private void ProcessPage(StringBuilder stringBuilder, Page page)
        {
            // aceder ao texto do pdf
            string text = page.Text;

            var split = text.Split(
                new[] { "Title:", "Date:", "Time:", "Visit us:" }, StringSplitOptions.None);

            var domain = ExtractDomain(split.Last());
            var ticketCulture = _domainCulture[domain];

            for (int i = 1; i < split.Length - 3; i += 3)
            {
                var title = split[i];
                var dateAsString = split[i + 1];
                var timeAsString = split[i + 2];

                var date = DateOnly.Parse(
                    dateAsString, new CultureInfo(ticketCulture));
                var time = TimeOnly.Parse(
                    timeAsString, new CultureInfo(ticketCulture));

                var dateAsStringInvariant = date
                    .ToString(CultureInfo.InvariantCulture);
                var timeAsStringInvariant = time
                    .ToString(CultureInfo.InvariantCulture);

                var ticketData =
                    $"{title,-40}|{dateAsStringInvariant}|{timeAsStringInvariant}";

                // adiciona-se à stringBuilder, linha a linha
                stringBuilder.AppendLine(ticketData);
            }
        }

        private static string ExtractDomain(string webAdress)
        {
            var lastDotIndex = webAdress.LastIndexOf('.');
            return webAdress.Substring(lastDotIndex);
        }
    }
}
