using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using InventorShims;

namespace InventorShims
{
    public static class TestClass
    {
        public static string Test()
        {
            return PathShim.UpOneLevel("C:/A/Test/Path/And/Stuff");
        }
    }
}
