using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GPW
{

    /// <summary>
    /// Stato di un processo
    /// </summary>
    public enum ProcessState
    {
        Running,
        NotRunning
    }

    /// <summary>
    /// EventArgs per cambio stato processo
    /// </summary>
    public class ProcessStateChangedEventArgs : EventArgs
    {
        public string ProcessNamePattern { get; }
        public ProcessState State { get; }

        public ProcessStateChangedEventArgs(string processNamePattern, ProcessState state)
        {
            ProcessNamePattern = processNamePattern;
            State = state;
        }
    }


    /// <summary>
    /// Listener singolo per un processo
    /// </summary>
    public class ProcessListener : IDisposable
    {
        public string ProcessPattern { get; }
        public Regex RegexPattern { get; } = null;


        public ProcessState CurrentState { get; private set; } = ProcessState.NotRunning;

        public event EventHandler<ProcessStateChangedEventArgs> ProcessStateChanged;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly TimeSpan _interval;

        public ProcessListener(string ProcessPattern, TimeSpan? checkInterval = null)
        {
            this.ProcessPattern = ProcessPattern;
            if( ProcessPattern.StartsWith("^"))
                RegexPattern = new Regex(ProcessPattern, RegexOptions.IgnoreCase);

            _interval = checkInterval ?? TimeSpan.FromSeconds(2); // default 2 secondi
        }

        public void Start()
        {
            Task.Run(() => MonitorLoop(_cts.Token));
        }

        private async Task MonitorLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                bool isRunning = Process.GetProcesses()
                               .Any(p =>
                               {
                                   try
                                   {             
                                       if(RegexPattern == null )
                                       {
                                           return string.Equals(p.ProcessName, ProcessPattern, StringComparison.OrdinalIgnoreCase);
                                       }
                                       return RegexPattern.IsMatch(p.ProcessName);
                                   }
                                   catch
                                   {
                                       return false; // ignora processi non accessibili
                                   }
                               });


           
                var newState = isRunning ? ProcessState.Running : ProcessState.NotRunning;

                if (newState != CurrentState)
                {
                    CurrentState = newState;
                    ProcessStateChanged?.Invoke(this, new ProcessStateChangedEventArgs(ProcessPattern, CurrentState));
                }

                await Task.Delay(_interval, token);
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
