using System;
using System.Runtime.InteropServices;
using Inventor;


namespace InventorShims
{
    public sealed class GetInventor
    {
        private static GetInventor instance = null;
        private static readonly object padlock = new object();
        public static Inventor.Application Application;
        private bool _started = false;

        GetInventor()
        {


            try
            {
                Application = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
            }
            catch (Exception e)
            {
                try
                {
                    Type invAppType = Type.GetTypeFromProgID("Inventor.Application");
                    Application = (Inventor.Application)Activator.CreateInstance(invAppType);
                    Application.Visible = true;
                    _started = true;

                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2.ToString());
                    Console.WriteLine("Unable to get or start Inventor");
                    _started = false;
                }
            }

        }

        public static GetInventor Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GetInventor();
                    }

                    return instance;
                }
            }
        }

    }
}
