using TicketsDataAggregator.TicketsAggregation;

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
                    TicketsFolder,
                    new FileWriter(),
                    new DocumentsFromPdfsReader());

                ticketsAggregator.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured."+
                    "Exception message: " + ex.Message);
            }
         }
    }
}
