using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PropertyChanged;

namespace DoLess.Commands.UwpSample
{
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        public MainViewModel()
        {
            this.Command1 = Command.CreateFromAction(this.ExecuteCommand1);
            this.Command2 = Command.CreateFromTask(this.ExecuteCommand2);
            this.Command3 = Command.CreateFromTask(this.ExecuteCommand3);            
        }

        public string Status { get; set; }

        public ICommand Command1 { get;}
        public ICommand Command2 { get; }
        public ICommand Command3 { get; }

        private void ExecuteCommand1()
        {
            this.Status = "Start Command1";
            Task.Delay(3000).Wait();
            this.Status = "End Command1";
        }

        private async Task ExecuteCommand2()
        {
            this.Status = "Start Command2";
            await Task.Delay(3000);
            this.Status = "End Command2";
        }
        private async Task ExecuteCommand3(CancellationToken ct)
        {
            this.Status = "Start Command3";
            await Task.Delay(3000, ct);
            this.Status = "End Command3";
        }
    }
}
