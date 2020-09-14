using System.IO;
using System.Linq;
using System.Text;

namespace AutomatosData
{
    public class TuringMachine
    {
        private string programName;
        private TuringMachineData first;
        private string state;
        private string[] finalStates;
        private TuringMachineInstruction[] instructions;

        public TuringMachine() { }

        public void SetupMachineFromPath(string path)
        {
            string[] lines = File.ReadAllLines(path);
            
            programName = lines[0].Substring(lines[0].Length - (lines[0].Length - 1));

            string tape = lines[1].Split(" ")[1];
            BuildTapeFromString(tape);
            
            state = lines[2].Split(" ")[1];

            for (int i = 1; i < lines[3].Split(" ").Length; i++)
            {
                finalStates.Append(lines[3].Split(" ")[i]);
            }

            for (int i = 4; i < lines.Length; i++)
            {
                if (lines[i] != "")
                {
                    TuringMachineInstruction newInstruction = new TuringMachineInstruction();
                    newInstruction.SetupInstructionFromString(lines[i]);
                    instructions.Append(newInstruction);
                }
            }
        }

        private void BuildTapeFromString(string tape)
        {
            // TuringMachineData oldItem = null;
            //
            // foreach (var c in tape)
            // {
            //     TuringMachineData tapeItem = new TuringMachineData(c.ToString());
            //
            //     if (oldItem != null)
            //     {
            //         oldItem.Next = tapeItem;
            //         tapeItem.Prev = oldItem;
            //         oldItem = tapeItem;
            //     }
            //     else
            //     {
            //         first = tapeItem;
            //         oldItem = tapeItem;
            //     }
            // }
        }
    }
}