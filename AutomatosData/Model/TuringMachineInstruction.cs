using System;
using System.Linq;
using System.Text;

namespace AutomatosData
{
    // public class TuringMachineInstruction
    // {
    //     private string currentState;
    //     private string tapeIndexCondition;
    //     private string nextState;
    //     private string tapeIndexInput;
    //     private string movementDirection;
    //     
    //     public TuringMachineInstruction() { }
    //
    //     public TuringMachineInstruction SetupInstructionFromString(string instruction)
    //     {
    //         string[] insts = instruction.Split(";");
    //
    //         currentState = insts[1];
    //         tapeIndexCondition = insts[2];
    //         nextState = insts[3];
    //         tapeIndexInput = insts[4];
    //         movementDirection = insts[5];
    //
    //         var entry = new TuringMachineDataEntry();
    //         
    //         return this;
    //     }
    // }

    public class TuringMachineInstruction
    {
        public enum Movement { Left, Right }
        
        public string InstructionState { get; }
        public char InstructionData { get; }
        public string InstructionExitState { get; }
        public char InstructionOutput { get; }
        public Movement InstructionMovement { get; }

        public bool IsValid(TuringMachine machine)
        {
            return machine.CurrentState == InstructionState &&
                   machine.Data.CurrentData == InstructionData;
        }
        
        private TuringMachineInstruction(string instructionState, 
            char instructionData, 
            string instructionExitState, 
            char instructionOutput, 
            Movement instructionMovement)
        {
            InstructionState = instructionState;
            InstructionData = instructionData;
            InstructionExitState = instructionExitState;
            InstructionOutput = instructionOutput;
            InstructionMovement = instructionMovement;
        }

        public static TuringMachineInstruction FromString(string source)
        {
            //Clear input
            var input = source
                .Replace(" ", string.Empty);

            var inputs = input.Split(',');
            
            //Validation
            if (inputs.Length != 5)
                throw new ArgumentException($"Invalid input string: {source}", nameof(source));
            
            if (string.IsNullOrEmpty(inputs[0]))
                throw new ArgumentException($"Instruction state cannot be null: {source}", nameof(source));

            if (inputs[1].Length != 1)
                throw new ArgumentException($"Instruction Data must be char: {source}", nameof(source));
            
            if (string.IsNullOrEmpty(inputs[2]))
                throw new ArgumentException($"Instruction Exit state cannot be null: {source}", nameof(source));
            
            if (inputs[3].Length != 1)
                throw new ArgumentException($"Instruction Output must be char: {source}", nameof(source));
            
            var instructionStage = inputs[0];
            var instructionData = inputs[1].First();
            var instructionExitState = inputs[2];
            var instructionOutput = inputs[3].First();
            var instructionMovement = inputs[4] == "<" ? Movement.Left :
                inputs[4] == ">" ? Movement.Right : throw new ArgumentException($"Movement Direction must be '>' or '<': {source}", nameof(source));
            
            return new TuringMachineInstruction(
                instructionStage, 
                instructionData, 
                instructionExitState, 
                instructionOutput, 
                instructionMovement);
        }
    }
}