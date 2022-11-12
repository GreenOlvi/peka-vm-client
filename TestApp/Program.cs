using PekaVMClient;

namespace TestApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await using var client = new VMClient();
            await RunGetLines(client, "14");
        }

        private static async Task RunGetLines(VMClient client, string pattern)
        {
            var lines = await client.GetLinesAsync(pattern);
            Console.WriteLine($"Lines matching pattern '{pattern}': " + string.Join(", ", lines));
        }
    }
}