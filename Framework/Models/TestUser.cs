namespace Framework.Models
{
    using Framework.Core.Enums;

    public class TestUser
    {
        public TestUser(string firstName, string lastName, Gender gender)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }
    }
}