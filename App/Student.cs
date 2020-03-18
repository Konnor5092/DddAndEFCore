namespace App
{
    public class Student
    {
        public long Id { get; }
        public string Name { get; }
        public string Email { get; }
        public virtual Course FavoriteCourse { get; }

        protected Student() { }

        public Student(string name, string email, Course favoriteCourse)
        {
            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;
        }
    }
}