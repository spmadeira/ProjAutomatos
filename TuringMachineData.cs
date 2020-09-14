namespace DefaultNamespace
{
    public class TuringMachineData
    {
        private TuringMachineData prev;
        private TuringMachineData next;
        private string data;

        public TuringMachineData(string data)
        {
            this.data = data;
        }

        public TuringMachineData Prev { get; set; }

        public TuringMachineData Next { get; set; }

        public string Data { get; set; }
    }
}