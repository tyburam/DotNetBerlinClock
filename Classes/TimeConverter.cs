using System.Text;

namespace BerlinClock
{
    public class BerlinClock : ITimeConverter
    {
        public string ConvertTime(string time)
        {
            int[] rowLen = new int[4] { 4, 4, 11, 4};
            char[] colors = new char[5] {'Y', 'R', 'R', 'Y', 'Y'};

            var components = time.Split(':');
            int seconds = int.Parse(components[2]), hours = int.Parse(components[0]), minutes = int.Parse(components[1]);
            int[] rowOn = new int[] {
                hours / 5,
                hours % 5,
                minutes / 5,
                minutes % 5
            };

            var clockValue = new StringBuilder("O\nOOOO\nOOOO\nOOOOOOOOOOO\nOOOO");
            if(seconds % 2 == 0)
            {
                clockValue[0] = colors[0];
            }

            var clockPos = 2;
            for(var i = 0; i < 4; i++) {
                ProcessRow(clockValue, ref clockPos, rowLen[i], rowOn[i], colors[i+1]);
            }

            //color fix for 3rd row
            clockPos = 14;
            for(var i = 0; i < rowOn[2] / 3; i++) {
                clockValue[clockPos] = colors[1];
                clockPos += 3;
            }

            return clockValue.ToString();
        }

        private void ProcessRow(StringBuilder sb, ref int clockPos, int rowLen, int rowOn, char color)
        {
            for(var i = 0; i < rowLen; i++) 
            {
                if(i < rowOn) {
                    sb[clockPos] = color;
                }
                ++clockPos;
            }
            ++clockPos; //passing new line
        }
    }
}
