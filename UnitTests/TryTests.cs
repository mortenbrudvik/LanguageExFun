using System;
using FluentAssertions;
using LanguageExt;
using Moq;
using Xunit;

using static LanguageExt.Prelude;

namespace UnitTests
{
    public class TryTests
    {
        [Fact]
        public void Exception_is_thrown_and_captured_by_try()
        {
            var personRepo = new Mock<IPersonRepository>();
            personRepo.Setup(x => x.Get(It.IsAny<Guid>()))
                .Throws(new Exception("yikes!"));
            var sut = new PersonController(personRepo.Object);
            
            sut.GetPerson(Guid.NewGuid())
                .IsFail()
                .Should().BeTrue();
        }
    }
    public class PersonController
    {
        private readonly IPersonRepository _repository;

        public PersonController(IPersonRepository repository) => _repository = repository;

        public Try<Person> GetPerson(Guid id) =>
            Try(() => _repository.Get(id));
    }
    public interface IPersonRepository
    {
        Person Get(Guid id);
    }
    public record Person(Guid Id);
}