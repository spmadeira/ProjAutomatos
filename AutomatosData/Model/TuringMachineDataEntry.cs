using System.Collections;
using System.Collections.Generic;

namespace AutomatosData
{
    public class TuringMachineData : IEnumerable<TuringMachineDataEntry>
    {
        public TuringMachineDataEntry Center;

        public TuringMachineData()
        {
            Center = new TuringMachineDataEntry();
        }

        public TuringMachineData(string initialData) : this()
        {
            var entry = Center;
            for (int i = 0; i < initialData.Length; i++)
            {
                entry.Data = initialData[i];

                //Não criar um item extra
                if (i != initialData.Length - 1)
                    entry = entry.Next;
            }
        }

        public TuringMachineDataEntry this[int index]
        {
            get
            {
                var entry = Center;
                while (index > 0)
                {
                    index -= 1;
                    entry = entry.Next;
                }

                while (index < 0)
                {
                    index += 1;
                    entry = entry.Prev;
                }

                return entry;
            }
        }

        public IEnumerator<TuringMachineDataEntry> GetEnumerator()
        {
            var entry = Center;

            //Go to first
            while (entry._prev != null)
                entry = entry._prev;

            //Go to last
            while (entry != null)
            {
                yield return entry;
                entry = entry._next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class TuringMachineDataEntry
    {
        internal TuringMachineDataEntry _prev;
        internal TuringMachineDataEntry _next;
        
        public TuringMachineDataEntry Prev => _prev ??= new TuringMachineDataEntry{_next = this};
        public TuringMachineDataEntry Next => _next ??= new TuringMachineDataEntry{_prev = this};

        public char Data { get; set; }

        public TuringMachineDataEntry() : this('_')
        {
        }

        public TuringMachineDataEntry(char data) => Data = data;
    }
}