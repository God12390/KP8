using System;

namespace KP8
{
    internal class Player : Person
    {
        public decimal Salary { get; set; } = 0;
        public bool Status { get; set; }
        public bool HealthStatus { get; set; }

        public Player(string firstName, string lastName, DateTime dateOfBirth, bool health) : base(firstName, lastName, dateOfBirth)
        {
            Salary = 0;
            Status = false;
            HealthStatus = health;
        }

        public override string ToString()
        {
            string status = Status ? "has a team" : "has no team";
            string healthStatus = HealthStatus ? "healthy" : "not healthy";
            if (Status)
                return $"Name: {FirstName} | Surname: {LastName} | Status: {status} | Health status: {healthStatus} | Salary: {Salary} | Date of Birth: {DateOfBirth}";
            return $"Name: {FirstName} | Surname: {LastName} | Status: {status} | Health status: {healthStatus} | Date of Birth: {DateOfBirth}";

        }
    }
}