using System.Linq;
using AutomatosData;
using NUnit.Framework;

namespace AutomatosTest
{
    public class Tests
    {
        [SetUp] public void Setup()
        {

        }

        [Test(Description = "Should create and properly index Turing Machine Data")] public void Test1()
        {
            var turingMachineData = new TuringMachineData("1011");
            var data = new string(turingMachineData.ReadAll());

            //Confere que a data na maquina é 1011
            Assert.AreEqual("1011", data);

            //Confere que tem 4 entradas
            Assert.AreEqual(4, turingMachineData.Count());
            
            //Confere cada digito manualmente
            Assert.AreEqual('1', turingMachineData[0].Data);
            Assert.AreEqual('0', turingMachineData[1].Data);
            Assert.AreEqual('1', turingMachineData[2].Data);
            Assert.AreEqual('1', turingMachineData[3].Data);
        }

        [Test(Description = "Can append data before and after center")] public void Test2()
        {
            var turingMachineData = new TuringMachineData("0");
            turingMachineData[-2].Data = '0';
            turingMachineData[-1].Data = '1';
            turingMachineData[1].Data = '1';
            turingMachineData[2].Data = '0';
            
            var data = new string(turingMachineData.Select(e => e.Data).ToArray());
            
            //Confere que a data na maquina é 01010
            Assert.AreEqual("01010", data);
            
            //Confere cada index
            Assert.AreEqual('0', turingMachineData[-2].Data);
            Assert.AreEqual('1', turingMachineData[-1].Data);
            Assert.AreEqual('0', turingMachineData[0].Data);
            Assert.AreEqual('1', turingMachineData[1].Data);
            Assert.AreEqual('0', turingMachineData[2].Data);
        }

        [Test(Description = "Append Y to end and X to start")] public void Test3()
        {
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
            
            Assert.AreEqual(TuringMachine.FinishResult.Valid, turingMachine.Result);
            Assert.AreEqual("X1011Y", turingMachine.Data.ReadAll());
        }
        
        [Test(Description = "Add 1 to binary number")] public void Test4()
        {
            var input =
                @"
@Programa Fonte Unifor
fita 1011
init qi
accept qf

qi,1,qi,1,>
qi,0,qi,0,>
qi,_,q1,_,<
q1,1,q1,0,<
q1,0,qf,1,<";
            
            var turingMachine = TuringMachine.FromText(input.Split('\n'));
            
            while(turingMachine.MachineState != TuringMachine.State.Finished)
                turingMachine.Run();
            
            Assert.AreEqual(TuringMachine.FinishResult.Valid, turingMachine.Result);
            Assert.AreEqual("1100", turingMachine.Data.ReadAll());
        }
    }
}