namespace HelloWorld
{
    internal class Program
    {
        private static IEnumerable<Alpha> GetCollection()
        {
            var item1 = new Alpha(3);
            var item2 = new Alpha(2);
            var item3 = new Alpha(5);

            yield return item1;
            yield return item2;
            yield return item3;
        }
        static void Main(string[] args)
        {
            /*
            Student student1 = new Student("Hmurkin", "George", "GGG", "M8O-211B-21", Student.practice_course_mode.C);
            Student student2 = new Student("FFFFF", "IIIII", "OOOOO", "M8O-322B-21", Student.practice_course_mode.Go);
            Console.WriteLine(student1); 
            Console.WriteLine(student2.Equals(student1));
            Console.WriteLine(student2.Course);
            */

            var col = GetCollection().generate_permutations<Alpha>(Compare.Instatnce);
            foreach (var item in col)
            {
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2._size);

                }
            }

        }
    }
}
