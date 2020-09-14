namespace proj_automatos
{
    public class TuringMachineInstruction
    {
        private string currentState;
        private string tapeIndexCondition;
        private string nextState;
        private string tapeIndexInput;
        private string movementDirection;
        
        public TuringMachineInstruction() { }

        public TuringMachineInstruction SetupInstructionFromString(string instruction)
        {
            string[] insts = instruction.Split(";");

            currentState = insts[1];
            tapeIndexCondition = insts[2];
            nextState = insts[3];
            tapeIndexInput = insts[4];
            movementDirection = insts[5];

            return this;
        }
    }
}