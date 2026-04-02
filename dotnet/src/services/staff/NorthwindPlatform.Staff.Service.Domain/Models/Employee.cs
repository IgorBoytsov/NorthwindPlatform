using NorthwindPlatform.Staff.Service.Domain.Events.Employees;
using NorthwindPlatform.Staff.Service.Domain.ValueObjects.Employees;
using Shared.Kernel.Primitives;

namespace NorthwindPlatform.Staff.Service.Domain.Models
{
    public sealed class Employee : AggregateRoot<EmployeeId>
    {
        public UserId UserId { get; private set; }
        public AccountStatus AccountStatus { get; private set; }
        public Name Name { get; private set; }
        public Surname Surname { get; private set; } 
        public Patronymic? Patronymic { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public Citizenship Citizenship { get; private set; }

        private Employee()
        {
            
        }

        private Employee(UserId userId, AccountStatus accountStatus, Name name, Surname surname, Patronymic? patronymic, DateOnly dateOfBird, Gender gender, Citizenship citizenship) : base(EmployeeId.New())
        {
            UserId = userId;
            AccountStatus = accountStatus;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            DateOfBirth = dateOfBird;
            Gender = gender;
            Citizenship = citizenship;
            
            AddDomainEvent(new EmployeeCreated(Id, UserId, AccountStatus, Name, Surname, Patronymic, DateOfBirth.ToString(), Gender, Citizenship));
        }

        public static Employee Create(UserId userId, AccountStatus accountStatus, Name name, Surname surname, Patronymic? patronymic, DateOnly dateOfBird, Gender gender, Citizenship citizenship)
        {
            return new Employee(userId, accountStatus, name, surname, patronymic, dateOfBird, gender, citizenship);
        }
    }
}