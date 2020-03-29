using AspNetCoreWebApi.Models;
using AspNetCoreWebApi.Repositories;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreWebApiTests.Repositories
{
    public class CourseRepositoryTests
    {
        IFixture _fixture;
        public CourseRepositoryTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void CtorArgumentNullGuardTests()
        {
            Arrange();
            Asserts();

            GuardClauseAssertion assertions;
            void Arrange()
            {
                assertions = new GuardClauseAssertion(_fixture);
            }
            void Asserts()
            {
                assertions.Verify(typeof(CourseRepository).GetConstructors());
            }
        }

        [Theory, AutoData]
        public async Task Get(long id)
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseRepository sut;
            void Arrange()
            {
                sut = _fixture.Create<CourseRepository>();
            }
            
            Task<Course> resultTask;
            void Action()
            {
                resultTask = sut.Get(id);
            }
            
            async Task Asserts()
            {
                var result = await resultTask.ConfigureAwait(false);
                Assert.NotNull(result);
                Assert.Equal(id, result.CourseId);
            }
        }

        [Fact]
        public async Task List()
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseRepository sut;
            void Arrange()
            {
                sut = _fixture.Create<CourseRepository>();
            }

            Task<IEnumerable<Course>> resultTask;
            void Action()
            {
                resultTask = sut.List();
            }

            async Task Asserts()
            {
                var result = await resultTask.ConfigureAwait(false);
                Assert.NotNull(result);
                Assert.NotEmpty(result);
                Assert.IsAssignableFrom<IEnumerable<Course>>(result);
            }
        }

        [Theory, AutoData]
        public void Update(long id, Course update)
        {
            Arrange();
            Asserts();

            CourseRepository sut;
            void Arrange()
            {
                sut = _fixture.Create<CourseRepository>();
            }

            void Asserts()
            {
                Assert.ThrowsAsync<NotImplementedException>(() => sut.Update(id, update));
            }
        }

        [Fact]
        public void UpdateArgumentNullGuardTests()
        {
            Arrange();
            Asserts();

            GuardClauseAssertion assertions;
            void Arrange()
            {
                assertions = new GuardClauseAssertion(_fixture);
            }
            void Asserts()
            {
                assertions.Verify(typeof(CourseRepository).GetMethod("Update"));
            }
        }
    }
}
