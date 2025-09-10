using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPW
{
      /// <summary>
    /// Gestore multiplo di listener
    /// </summary>
    public class ProcessManager : IDisposable
    {
        private readonly List<ProcessListener> _listeners = new List<ProcessListener>();

        public void AddListener(ProcessListener listener)
        {
            _listeners.Add(listener);
            listener.Start();
        }

        public IEnumerable<ProcessListener> GetListeners() => _listeners;

        public void removeListener(ProcessListener listener)
        {
            if (_listeners.Contains(listener))
            {
                listener.Dispose();
                _listeners.Remove(listener);
            }
        }

        public void ClearListeners()
        {
            foreach (var listener in _listeners)
                listener.Dispose();
            _listeners.Clear();
        }

        public void Dispose()
        {
            foreach (var listener in _listeners)
                listener.Dispose();
        }
    }
}
