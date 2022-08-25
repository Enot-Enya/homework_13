using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_12
{
    class SomeException:Exception
    {
        public override string Message => "Баланс не может быть отрицательным";
    }
}
