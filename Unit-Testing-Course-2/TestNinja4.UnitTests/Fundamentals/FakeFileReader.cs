using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja4.Mocking;

namespace TestNinja.UnitTests
{
    public class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "";
        }
    }
}
