using Routing;

internal class Program
{
    private static void Main(string[] args)
    {
        Router router = new Router();

        router.RegisterRoute("/foo/bar/", () => { Console.WriteLine("WithoutArguments"); });
        router.RegisterRoute<int>("/foo/bar/{a:int}/", (a) => { Console.WriteLine(a); });
        router.RegisterRoute<int, int>("/foo/bar/{a:int}/{b:int}/", (a, b) => { Console.WriteLine($"{a} | {b}"); });

        router.Route("/foo/bar/");
        router.Route("/foo/bar/{a:int}/");
        router.Route("/foo/bar/{a:int}/{b:int}/");
    }
}