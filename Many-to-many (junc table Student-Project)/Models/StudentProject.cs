namespace Many_to_many__junc_table_Student_Project_.Models
{
    public class StudentProject
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public double Grade { get; set; }  //оцінка студента за проект
    }
}