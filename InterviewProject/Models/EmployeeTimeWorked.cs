namespace InterviewProject.Models {
    public class EmployeeTimeWorked {
        public string Name { get; set; }
        public float TimeWorked { get; set; }

        public EmployeeTimeWorked(string name, float time)
        {
            Name = name;
            TimeWorked = time;
        }
    }
}
