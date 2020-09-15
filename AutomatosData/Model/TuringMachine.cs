using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutomatosData
{
    // public class TuringMachine
    // {
    //     private string programName;
    //     private TuringMachineData data;
    //     private string state;
    //     private string[] finalStates;
    //     private TuringMachineInstruction[] instructions;
    //
    //     public TuringMachine() { }
    //
    //     public void SetupMachineFromPath(string path)
    //     {
    //         // string[] lines = File.ReadAllLines(path);
    //         //
    //         // programName = lines[0].Substring(lines[0].Length - (lines[0].Length - 1));
    //         //
    //         // string tape = lines[1].Split(" ")[1];
    //         // BuildTapeFromString(tape);
    //         //
    //         // state = lines[2].Split(" ")[1];
    //         //
    //         // for (int i = 1; i < lines[3].Split(" ").Length; i++)
    //         // {
    //         //     finalStates.Append(lines[3].Split(" ")[i]);
    //         // }
    //         //
    //         // for (int i = 4; i < lines.Length; i++)
    //         // {
    //         //     if (lines[i] != "")
    //         //     {
    //         //         TuringMachineInstruction newInstruction = new TuringMachineInstruction();
    //         //         newInstruction.SetupInstructionFromString(lines[i]);
    //         //         instructions.Append(newInstruction);
    //         //     }
    //         // }
    //     }
    //
    //     private void BuildTapeFromString(string tape)
    //     {
    //         // TuringMachineData oldItem = null;
    //         //
    //         // foreach (var c in tape)
    //         // {
    //         //     TuringMachineData tapeItem = new TuringMachineData(c.ToString());
    //         //
    //         //     if (oldItem != null)
    //         //     {
    //         //         oldItem.Next = tapeItem;
    //         //         tapeItem.Prev = oldItem;
    //         //         oldItem = tapeItem;
    //         //     }
    //         //     else
    //         //     {
    //         //         first = tapeItem;
    //         //         oldItem = tapeItem;
    //         //     }
    //         // }
    //     }
    // }

    public class TuringMachine
    {
        public enum State { Ready, Running, Finished }
        public enum FinishResult { Valid, Invalid }
        
        public TuringMachineData Data { get; }
        public string CurrentState { get; private set; }
        public string[] GoalStates { get; }
        public TuringMachineInstruction[] Instructions { get; }
        public State MachineState { get; private set; }
        public FinishResult? Result { get; private set;  }

        private TuringMachine(TuringMachineData data, 
            string initialState,
            string[] goalStates, 
            TuringMachineInstruction[] instructions)
        {
            Data = data;
            CurrentState = initialState;
            GoalStates = goalStates;
            Instructions = instructions;
            MachineState = State.Ready;
            Result = null;
        }

        public static TuringMachine FromFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File Not Found", path);

            var lines = File.ReadAllLines(path);
            return FromText(lines);
        }

        public static TuringMachine FromText(string[] lines)
        {
            TuringMachineData data = null;
            string initialState = null;
            string[] goalStates = new string[0];
            List<TuringMachineInstruction> instructions = new List<TuringMachineInstruction>();
            
            foreach (var line in lines)
            {
                //Check if is comment
                var cleanedLine = line.Trim();
                
                if (cleanedLine.StartsWith("@"))
                    continue;

                if (cleanedLine.StartsWith("fita "))
                {
                    if (data != null)
                        throw new ArgumentException($"Cannot redefine data", nameof(lines));
                    data = new TuringMachineData(cleanedLine.Substring(5)); //Ignore 'fita '
                } else if (cleanedLine.StartsWith("init "))
                {
                    if (initialState != null)
                        throw new ArgumentException($"Cannot redefine initial state", nameof(lines));
                    initialState = cleanedLine.Substring(5);
                } else if (cleanedLine.StartsWith("accept "))
                {
                    if (goalStates.Any())
                        throw new ArgumentException($"Cannot redefine goal states", nameof(lines));
                    goalStates = cleanedLine.Substring(7).Trim().Split(",");
                }
                else if (!string.IsNullOrWhiteSpace(cleanedLine))
                {
                    try
                    {
                        var instruction = TuringMachineInstruction.FromString(cleanedLine);
                        instructions.Add(instruction);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ArgumentException($"Could not parse line {cleanedLine}: {e.Message}", e);
                    }
                }
            }
            
            return new TuringMachine(data, initialState, goalStates, instructions.ToArray());
        }

        public void Run()
        {
            if (MachineState == State.Ready)
                MachineState = State.Running;
            
            if (MachineState != State.Running)
                return;

            var instruction = Instructions.FirstOrDefault(i => i.IsValid(this));
            if (instruction == null)
            {
                Stop();
                return;
            }

            CurrentState = instruction.InstructionExitState;
            Data.Write(instruction.InstructionOutput);

            switch (instruction.InstructionMovement)
            {
                case TuringMachineInstruction.Movement.Left:
                    Data.MovePrev();
                    break;
                case TuringMachineInstruction.Movement.Right:
                    Data.MoveNext();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(instruction.InstructionMovement));
            }
        }

        private void Stop()
        {
            MachineState = State.Finished;
            Result = GoalStates.Contains(CurrentState) ? FinishResult.Valid : FinishResult.Invalid;
        }
    }
}