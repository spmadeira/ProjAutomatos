using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutomatosData;

namespace proj_automatos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var input =
                @"
@Programa Fonte Unifor
fita 1011
init qi
accept qf

qi,1,qi,1,>
qi,0,qi,0,>
qi,_,q1,Y,<
q1,1,q1,1,<
q1,0,q1,0,<
q1,_,qf,X,>";
            
            var turingMachine = TuringMachine.FromText(input.Split('\n'));
            
            while(turingMachine.MachineState != TuringMachine.State.Finished)
                turingMachine.Run();
        }
    }
}
