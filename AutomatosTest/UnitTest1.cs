using System;
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

        [TestCase(TestName = "Should create and properly index Turing Machine Data")]
        public void Test1()
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

        [TestCase(TestName = "Can append data before and after center")]
        public void Test2()
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

        [TestCase(TestName = "Append Y to end and X to start")]
        public void Test3()
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

            while (turingMachine.MachineState != TuringMachine.State.Finished)
                turingMachine.Run();

            Assert.AreEqual(TuringMachine.FinishResult.Valid, turingMachine.Result);
            Assert.AreEqual("X1011Y", turingMachine.Data.ReadAll());
        }

        [TestCase(TestName = "Add 1 to binary number")]
        public void Test4()
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

            while (turingMachine.MachineState != TuringMachine.State.Finished)
                turingMachine.Run();

            Assert.AreEqual(TuringMachine.FinishResult.Valid, turingMachine.Result);
            Assert.AreEqual("1100", turingMachine.Data.ReadAll());
        }

        [TestCase(TestName = "Is Divisible by 5")]
        public void Test5()
        {
            for (int i = 0; i < 128; i++)
            {
                var asBinaryString = Convert.ToString(i, 2);
                var input =
                    $@"
@Programa Fonte Unifor
fita {asBinaryString}
init q0
accept q0

q0,0,q0,0,>
q0,1,q1,1,>
q1,0,q2,0,>
q1,1,q3,1,>
q2,0,q4,0,>
q2,1,q0,1,>
q3,0,q1,0,>
q3,1,q2,1,>
q4,0,q3,0,>
q4,1,q4,1,>";
                var turingMachine = TuringMachine.FromText(input.Split('\n'));

                while (turingMachine.MachineState != TuringMachine.State.Finished)
                    turingMachine.Run();

                Console.WriteLine($"On {i} -- {asBinaryString}");
                Assert.AreEqual(i % 5 == 0, turingMachine.Result == TuringMachine.FinishResult.Valid);
            }
        }

        [TestCase(TestName = "Ends on 01")] 
        public void Test6()
        {
            for (int i = 0; i < 128; i++)
            {
                var asBinaryString = Convert.ToString(i, 2);
                var input =
                    $@"
@Programa Fonte Unifor
fita {asBinaryString}
init qs
accept qv

qs,0,qs,0,>
qs,1,qs,1,>
qs,_,qcf,_,<
qcf,1,qcs,1,<
qcs,0,qv,0,<";
                var turingMachine = TuringMachine.FromText(input.Split('\n'));

                while (turingMachine.MachineState != TuringMachine.State.Finished)
                    turingMachine.Run();

                Console.WriteLine($"On {i} -- {asBinaryString}");
                Assert.AreEqual(asBinaryString.EndsWith("01"), turingMachine.Result == TuringMachine.FinishResult.Valid);
            }
        }
        
        [TestCase(TestName = "Block copy input")] 
        public void Test7()
        {
            for (int i = 0; i < 128; i++)
            {
                var asBinaryString = Convert.ToString(i, 2);
                var input =
                    $@"
@Programa Fonte Unifor
@x = 0 but was alredy copied
@a = 0 but was first copied
@y = 1 but was already copied
@b = 1 but was first copy

fita {asBinaryString}
init qcf
accept qf

qcf,0,qaf0,x,>
qcf,1,qaf1,y,>

qaf0,0,qaf0,0,>
qaf0,1,qaf0,1,>
qaf0,_,qr,a,<
qaf1,0,qaf1,0,>
qaf1,1,qaf1,1,>
qaf1,_,qr,b,<

qr,0,qr,0,<
qr,1,qr,1,<
qr,a,qr,a,<
qr,b,qr,b,<
qr,x,qc,x,>
qr,y,qc,y,>

qc,0,qa0,x,>
qc,1,qa1,y,>
qc,a,qt,0,<
qc,b,qt,1,<

qa0,0,qa0,0,>
qa0,1,qa0,1,>
qa0,a,qa0,a,>
qa0,b,qa0,b,>
qa0,_,qr,0,<
qa1,0,qa1,0,>
qa1,1,qa1,1,>
qa1,a,qa1,a,>
qa1,b,qa1,b,>
qa1,_,qr,1,<

qt,x,qt,0,<
qt,y,qt,1,<
qt,_,qf,_,>";
                var turingMachine = TuringMachine.FromText(input.Split('\n'));

                while (turingMachine.MachineState != TuringMachine.State.Finished)
                    turingMachine.Run();

                Console.WriteLine($"On {i} -- {asBinaryString}");

                try
                {
                    Assert.AreEqual(asBinaryString + asBinaryString, turingMachine.Data.ReadAll());
                    Assert.AreEqual(TuringMachine.FinishResult.Valid, turingMachine.Result);
                }
                catch (AssertionException)
                {
                    Console.WriteLine("Failed. Log:");
                    Console.WriteLine(turingMachine.Log);
                    throw;
                }
            }
        }
        
        [TestCase(TestName = "Multiply input by 2")] 
        public void Test8()
        {
            for (int i = 0; i < 128; i++)
            {
                var asBinaryString = Convert.ToString(i, 2);
                var input =
                    $@"
@Programa Fonte Unifor
fita {asBinaryString}
init q
accept qf

q,0,q,0,>
q,1,q,1,>
q,_,qf,0,<";
                var turingMachine = TuringMachine.FromText(input.Split('\n'));

                while (turingMachine.MachineState != TuringMachine.State.Finished)
                    turingMachine.Run();

                Console.WriteLine($"On {i} -- {asBinaryString}");
                Assert.AreEqual(i*2, Convert.ToInt32(turingMachine.Data.ReadAll(), 2));
            }
        }
    }
}