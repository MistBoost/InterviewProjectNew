using InterviewProject.Models;

using System.Diagnostics;

namespace InterviewProject {
    public class DataLogicConverter {
        public List<EmployeeTimeWorked> Result { get; set; }
        public Dictionary<string, TimeSpan> EmployeeTimeDictionary { get; set; }

        public DataLogicConverter(List<TimeEntry> data)
        {
            EmployeeTimeDictionary = new Dictionary<string, TimeSpan>();

            Result = ExctractData(data);
            Result = SortArray(Result, 0, Result.Count - 1);
            Result.Reverse();
        }

        private List<EmployeeTimeWorked> ExctractData(List<TimeEntry> input)
        {
            List<EmployeeTimeWorked> result = new();

            int count = 0;

            foreach (var item in input)
            {

                var name = item.EmployeeName;
                var timeWorked = item.EndTimeUtc - item.StarTimeUtc;

                if (name != String.Empty && name != ";" && name != null && name != "null")
                {
                    Debug.WriteLine(count);
                    count++;
                    if (EmployeeTimeDictionary.ContainsKey(name))
                    {
                        EmployeeTimeDictionary[name] += timeWorked;
                    }
                    else
                    {
                        EmployeeTimeDictionary.Add(name, timeWorked);
                    }
                }
            }

            foreach (var item in EmployeeTimeDictionary)
            {
                var obj = new EmployeeTimeWorked(item.Key, (float)item.Value.TotalHours);
                result.Add(obj);
            }

            return result;
        }

        public List<EmployeeTimeWorked> SortArray(List<EmployeeTimeWorked> array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex].TimeWorked;
            while (i <= j)
            {
                while (array[i].TimeWorked < pivot)
                {
                    i++;
                }

                while (array[j].TimeWorked > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                SortArray(array, leftIndex, j);
            if (i < rightIndex)
                SortArray(array, i, rightIndex);
            return array;
        }
    }
}
