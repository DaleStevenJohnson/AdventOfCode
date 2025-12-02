using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2022
{
  
    public class Y2022D6 : ISolution
    {
       
        public List<string> TestData { get; set; }
        public List<string> PuzzleData { get; set; }
        public Y2022D6()
        {
            TestData = MyIO.ReadStringsFromFile(2022, 6, 1, true);
            PuzzleData = MyIO.ReadStringsFromFile(2022, 6, 1, false);
        }

        public bool QueueHasUniqueValues(Queue<string> _queue)
        {
            var queue = new Queue<string>(_queue) ;
            var count = queue.Count-1;
            for (int i = 0; i < count; i++)
            {
                var item = queue.Dequeue();
                if (queue.Contains(item))
                    return false;
            }
            return true;
        }

        public string PrintQueue(Queue<string> queue)
        {
            var result = string.Empty;
            foreach (var item in queue)
            {
                result += item;
            }
            return result;
        }

        public int GetPositionOfMarker(string dataStream, int markerSize)
        {
            
            Queue<string> queue = new Queue<string>();
            var count = 0;
            foreach (var letter in dataStream)
            {
                if (queue.Count == markerSize)
                {
                    if (QueueHasUniqueValues(queue))
                        break;
                    else
                        queue.Dequeue();
                }
                queue.Enqueue(letter.ToString());
                count++;
            }
            return count;
        }

        public string SolvePart1(List<string> data)
        {
            var dataStream = data[0];
            var count = GetPositionOfMarker(dataStream, 4);
            return count.ToString();
        }

        

        public string SolvePart2(List<string> data)
        {
            var dataStream = data[0];
            var count = GetPositionOfMarker(dataStream, 14);
            return count.ToString();
        }
    }
}
 