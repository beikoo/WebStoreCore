using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common
{
    public class FileNameManipulator
    {
        public static string GenerateName()
        {
            return Guid.NewGuid().ToString().Substring(0, 10);
        }
    }
}
