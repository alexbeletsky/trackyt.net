using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R1UnitTest.HtttpSimulator;

namespace WebApplication.Tests.Framework
{
    public class FixtutureInit : IDisposable
    {
        private HttpSimulator _simulator;
        private DbScript _script;

        public FixtutureInit(string uri)
        {
            _simulator = new HttpSimulator().SimulateRequest(new Uri(uri));
            _script = new DbScript();
        }


        public HttpSimulator Simulator 
        { 
            get { return _simulator; } 
        }

        #region IDisposable Members

        public void Dispose()
        {
            _simulator.Dispose();
            _script.Dispose();
        }

        #endregion
    }
}
