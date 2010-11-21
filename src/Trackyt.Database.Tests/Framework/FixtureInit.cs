using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R1UnitTest.HtttpSimulator;

namespace Trackyt.Core.Tests.Framework
{
    public class FixtureInit : IDisposable
    {
        private HttpSimulator _simulator;
        private DbSetup _setup;

        public FixtureInit(string uri)
        {
            _simulator = new HttpSimulator().SimulateRequest(new Uri(uri));
            _setup = new DbSetup();
        }


        public HttpSimulator Simulator 
        { 
            get { return _simulator; } 
        }

        public DbSetup Setup
        {
            get { return _setup; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            _simulator.Dispose();
            _setup.Dispose();
        }

        #endregion
    }
}
