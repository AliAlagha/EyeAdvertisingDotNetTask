using EyeAdvertisingDotNetTask.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeAdvertisingDotNetTask.Core.Exceptions
{
    public class InvalidLoginCredintialesException : Exception
    {
        public InvalidLoginCredintialesException()
            : base(Messages.InvalidLoginCredintiales)
        {
        }
    }
}
