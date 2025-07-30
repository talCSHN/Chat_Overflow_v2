using Server;

namespace LoadTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var tester = new LoadTester();
            await tester.Run(100);
        }
    }
}
