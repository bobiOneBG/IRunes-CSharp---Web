namespace Musaca.Web
{
    using SIS.MvcFramework;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            WebHost.Start(new StartUp());
        }
    }
}
