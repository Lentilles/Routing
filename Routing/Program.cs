using Routing;

internal class Program
{
    private static void Main(string[] args)
    {        
        
        Router router = new Router();
        AddIntTest(router);
        AddFloatTest(router);
        AddDateTimeTest(router);
        AddGuidTest(router);
        AddStringTest(router);
        AddMixTest(router);
    }

    private static void AddTestStaticRoute(Router router)
    {
        router.RegisterRoute("/foo/bar/", () => { Console.WriteLine("WithoutArguments"); });

        router.Route("/foo/bar/");
    }
    private static void AddIntTest(Router router)
    {
        router.RegisterRoute("/foo/bar/{a:int}/", (int a) => { Console.WriteLine(a); });
        router.RegisterRoute("/foo/bar/{a:int}/{b:int}/", (int a, int b) => { Console.WriteLine($"{a} | {b}"); });

        router.Route("/foo/bar/5/");
        router.Route("/foo/bar/5/6/");
    }
    private static void AddFloatTest(Router router)
    {
        router.RegisterRoute("/foo/bar/{a:float}/", (float a) => { Console.WriteLine(a); });
        router.RegisterRoute("/foo/bar/{a:float}/{b:float}/", (float a, float b) => { Console.WriteLine($"{a} | {b}"); });

        router.Route($"/foo/bar/5.5/");
        router.Route($"/foo/bar/{(5.5f).ToString()}/{(6.5f).ToString()}/");
    }

    private static void AddDateTimeTest(Router router)
    {
        router.RegisterRoute("/foo/bar/{a:DateTime}/", (DateTime a) => { Console.WriteLine(a); });
        router.RegisterRoute("/foo/bar/{a:DateTime}/{b:DateTime}/", (DateTime a, DateTime b) => { Console.WriteLine($"{a} | {b}"); });

        router.Route($"/foo/bar/{DateTime.Now.ToString()}/");
        router.Route($"/foo/bar/{DateTime.Now.ToString()}/{DateTime.Now.ToString()}/");
    }

    private static void AddGuidTest(Router router)
    {
        router.RegisterRoute("/foo/bar/{a:Guid}/", (Guid a) => { Console.WriteLine(a); });
        router.RegisterRoute("/foo/bar/{a:Guid}/{b:Guid}/", (Guid a, Guid b) => { Console.WriteLine($"{a} | {b}"); });

        router.Route($"/foo/bar/{Guid.NewGuid()}/");
        router.Route($"/foo/bar/{Guid.NewGuid()}/{Guid.NewGuid()}/");
    }

    private static void AddStringTest(Router router)
    {
        router.RegisterRoute("/foo/bar/{a:string}/", (string a) => { Console.WriteLine(a); });
        router.RegisterRoute("/foo/bar/{a:string}/{b:string}/", (string a, string b) => { Console.WriteLine($"{a} | {b}"); });

        router.Route($"/foo/bar/TestString/");
        router.Route($"/foo/bar/TestString/teststring/");
    }


    private static void AddMixTest(Router router)
    {
        router.RegisterRoute("/a/b/c/d/{a:int}/{b:float}/{c:DateTime}/{d:Guid}/{g:string}/", 
            (
                int a, 
                float b, 
                DateTime c, 
                Guid d, 
                string g
            ) => 
            {
                Console.WriteLine("MixTest"); 
                Console.WriteLine(a); 
                Console.WriteLine(b); 
                Console.WriteLine(c); 
                Console.WriteLine(d); 
                Console.WriteLine(g); 
            });


        var a = 1;
        var b = 2.5f;
        var c = DateTime.Now;
        var d = Guid.NewGuid();
        var g = "Test";


        router.Route($"/a/b/c/d/{a}/{b}/{c}/{d}/{g}/");
    }
}