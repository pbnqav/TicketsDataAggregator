using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;

namespace TicketsDataAggregator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string TicketsFolder = @"c:\ourcinema\tickets";

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

        public TicketsAggregator(string ticketsFolder)
        {
            _ticketsFolder = ticketsFolder;
        }

        public void Run()
        {
            using (PdfDocument document = PdfDocument.Open(@"C:\my-file.pdf"))
            {
                int pageCount = document.NumberOfPages;

                // Page number starts from 1, not 0.
                Page page = document.GetPage(1);

                decimal widthInPoints = page.Width;
                decimal heightInPoints = page.Height;

                string text = page.Text;
            }
        }
    }
}
