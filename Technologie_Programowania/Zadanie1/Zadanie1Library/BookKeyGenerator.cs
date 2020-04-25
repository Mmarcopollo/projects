using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1Library
{
    class BookKeyGenerator
    {
        static int key=0;
        public static int generateKey()
        {
            return ++key;
        }


    }
}
