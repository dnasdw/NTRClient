using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntrclient
{
    class CommandHistory
    {
        private List<string> history;
        private int currentIndex;

        public CommandHistory()
        {
            history = new List<string>();
            currentIndex = 0;
        }

        public void AddCommand(string cmd)
        {
            if (!(history.Count != 0 && history[history.Count - 1] == cmd))
                history.Add(cmd);
            currentIndex = history.Count;
        }

        public string GetPrevCmd()
        {
            string cmd = "";
            try
            {
                if (currentIndex > 0)
                    currentIndex--;
                cmd = history[currentIndex];
            }
            catch (System.ArgumentOutOfRangeException)
            {

            }
            return cmd;
        }

        public string GetNextCmd()
        {
            string cmd = "";
            try
            {
                if (currentIndex < history.Count)
                    currentIndex++;
                cmd = history[currentIndex];
            }
            catch (System.ArgumentOutOfRangeException)
            {

            }
            return cmd;
        }
    }
}
