using AspNetCoreWebApi.DBContexts;
using AspNetCoreWebApi.Models;
using AspNetCoreWebApi.Repositories;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreWebApiTests.Repositories
{
    public class CourseRepositoryTests : IDisposable
    {
        IFixture _fixture;
        private TuitionAgencyContext _context;

        public CourseRepositoryTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var options = new DbContextOptionsBuilder<TuitionAgencyContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create("CourseRepositoryTests"))
                .Options;
            var loggerFactory = _fixture.Create<ILoggerFactory>();
            _context = new TuitionAgencyContext(options, loggerFactory);
            _fixture.Register(() => _context);
        }

        public void Dispose()
        {
            _context.Dispose();
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
                Assert.Null(result);
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
                Assert.IsAssignableFrom<IEnumerable<Course>>(result);
            }
        }

        [Theory, AutoData]
        public async Task Update(long id)
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseRepository sut;
            Course update;
            void Arrange()
            {
                update = _fixture.Create<Course>();
                sut = _fixture.Create<CourseRepository>();
            }
            Task<Course> result;
            void Action()
            {
                result = sut.Update(id, update);
            }
            async Task Asserts()
            {
                var course = await result.ConfigureAwait(false);
                Assert.Null(course);
                //Assert.Equal(update.CourseId, course.CourseId);
                //Assert.Equal(update.Name, course.Name);
                //Assert.Equal(update.TuitionAgencyId, course.TuitionAgencyId);
                //Assert.Equal(update.Subjects.Count, course.Subjects.Count);
            }
        }

        [Theory, AutoData]
        public async Task UpdateArgumentNullGuardTests(long id)
        {
            Arrange();
            await Asserts().ConfigureAwait(false);

            CourseRepository sut;
            void Arrange()
            {
                sut = _fixture.Create<CourseRepository>();
            }
            async Task Asserts()
            {
                await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Update(id, null)).ConfigureAwait(false);
            }
        }

        [Fact]
        public async Task Create()
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseRepository sut;
            Course create;
            void Arrange()
            {
                create = _fixture
                            .Build<Course>()
                            .Without(course => course.TuitionAgency)
                            .Create();

                sut = _fixture.Create<CourseRepository>();
            }

            Task<Course> resultTask;
            void Action()
            {
                CancellationToken token = new CancellationToken();
                resultTask = sut.Create(create, token);
            }

            async Task Asserts()
            {
                var result = await resultTask.ConfigureAwait(false);
                Assert.NotNull(result);
                Assert.Equal(create.CourseId, result.CourseId);
            }
        }
    }
}
