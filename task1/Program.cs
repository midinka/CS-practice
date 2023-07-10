namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student("Hmurkin", "George", "GGG", "M8O-211B-21", Student.practice_course_mode.C);
            Student student2 = new Student("FFFFF", "IIIII", "OOOOO", "M8O-322B-21", Student.practice_course_mode.Go);
            Console.WriteLine(student1); 
            Console.WriteLine(student2.Equals(student1));
        }
    }
}
