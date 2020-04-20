using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi.Core.Interfaces;
using AspNetCoreWebApi.DTOs.Query;
using AspNetCoreWebApi.DTOs.Upsert;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreWebApiTests.Controllers
{
    public class CourseControllerTests
    {
        private readonly IFixture _fixture;
        public CourseControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            _fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
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
                assertions.Verify(typeof(CourseController).GetConstructors());
            }
        }

        [Fact]
        public async Task GetAll()
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseController sut;
            Mock<IRepository<Course>> mockedRepository;
            void Arrange()
            {
                mockedRepository = _fixture.Freeze<Mock<IRepository<Course>>>();
                mockedRepository.Setup(fake => fake.List(It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult(_fixture.CreateMany<Course>()));

                sut = _fixture.Create<CourseController>();
            }
            Task<ActionResult<IEnumerable<CourseQueryDto>>> resultTask;
            void Action()
            {
                resultTask = sut.Get();
            }
            async Task Asserts()
            {
                var result = await resultTask;
                Assert.NotNull(result);
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var values = Assert.IsAssignableFrom<IEnumerable<CourseQueryDto>>(okResult.Value);
                Assert.NotNull(values);
                Assert.NotEmpty(values.ToList());
                mockedRepository.VerifyAll();
            }
        }

        [Theory, AutoData]
        public async Task GetById(long courseId)
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseController sut;
            Mock<IRepository<Course>> mockedRepository;
            void Arrange()
            {
                mockedRepository = _fixture.Freeze<Mock<IRepository<Course>>>();
                mockedRepository.Setup(fake => fake.Get(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult(_fixture.Build<Course>().With(course => course.CourseId, courseId).Create()));
                sut = _fixture.Create<CourseController>();
            }
            Task<ActionResult<CourseQueryDto>> resultTask;
            void Action()
            {
                resultTask = sut.GetById(courseId);
            }
            async Task Asserts()
            {
                var result = await resultTask.ConfigureAwait(false);
                Assert.NotNull(result);
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var course = Assert.IsType<CourseQueryDto>(okResult.Value);
                Assert.Equal(courseId, course.CourseId);
                mockedRepository.VerifyAll();
            }
        }

        [Theory, AutoData]
        public async Task Update(long courseId, CourseDto update)
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseController sut;
            Mock<IRepository<Course>> mockedRepository;
            void Arrange()
            {
                mockedRepository = _fixture.Freeze<Mock<IRepository<Course>>>();
                mockedRepository.Setup(fake => fake.Update(It.IsAny<long>(), It.IsAny<Course>(), It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult(_fixture.Build<Course>()
                                                                .With(course => course.CourseId, courseId)
                                                                .With(course => course.Name, update.Name)
                                                                .With(course => course.TuitionAgencyId, update.TuitionAgencyId)
                                                                .Create()));
                sut = _fixture.Create<CourseController>();

            }
            Task<ActionResult<CourseQueryDto>> resultTask;
            void Action()
            {
                resultTask = sut.Update(courseId, update);
            }
            async Task Asserts() 
            {
                var result = await resultTask.ConfigureAwait(false);
                Assert.NotNull(result);
                var okAcceptedResult = Assert.IsType<AcceptedAtActionResult>(result.Result);
                Assert.Equal(nameof(CourseController.GetById), okAcceptedResult.ActionName);
                var course = Assert.IsType<CourseQueryDto>(okAcceptedResult.Value);
                Assert.Equal(courseId, course.CourseId);
                Assert.Equal(update.Name, course.Name);
                Assert.Equal(update.TuitionAgencyId, course.TuitionAgencyId);
                mockedRepository.VerifyAll();
            }
        }

        [Theory, AutoData]
        public async Task Create(CourseDto create)
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseController sut;
            Mock<IRepository<Course>> mockedRepository;
            void Arrange()
            {
                mockedRepository = _fixture.Freeze<Mock<IRepository<Course>>>();
                mockedRepository.Setup(fake => fake.Create(It.IsAny<Course>(), It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult(_fixture.Build<Course>()
                                                                .With(course => course.CourseId, create.CourseId)
                                                                .With(course => course.Name, create.Name)
                                                                .With(course => course.TuitionAgencyId, create.TuitionAgencyId)
                                                                .Create()));
                sut = _fixture.Create<CourseController>();

            }
            Task<ActionResult<CourseQueryDto>> resultTask;
            void Action()
            {
                resultTask = sut.Create(create);
            }
            async Task Asserts()
            {
                var result = await resultTask.ConfigureAwait(false);
                Assert.NotNull(result);
                var okCreatedResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                Assert.Equal(nameof(CourseController.GetById), okCreatedResult.ActionName);
                var course = Assert.IsType<CourseQueryDto>(okCreatedResult.Value);
                Assert.Equal(create.CourseId, course.CourseId);
                Assert.Equal(create.Name, course.Name);
                Assert.Equal(create.TuitionAgencyId, course.TuitionAgencyId);
                mockedRepository.VerifyAll();
            }
        }
    }
}
