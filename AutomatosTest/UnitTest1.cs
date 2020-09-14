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
            var data = new string(turingMachineData.Select(e => e.Data).ToArray());

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
    }
}