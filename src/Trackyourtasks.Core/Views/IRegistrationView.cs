using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trackyourtasks.Core.Views
{
    public interface IRegistrationView
    {
        void Success();

        void Fail(string p);
    }
}
