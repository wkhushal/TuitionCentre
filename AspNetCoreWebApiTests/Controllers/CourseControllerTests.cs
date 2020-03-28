using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi.Core.Interfaces;
using AspNetCoreWebApi.DTOs.Query;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreWebApiTests.Controllers
{
    public class CourseControllerTests
    {
        IFixture _fixture;
        public CourseControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            _fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
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
            Mock<ICourseRepository> mockedRepository;
            void Arrange()
            {
                mockedRepository = _fixture.Freeze<Mock<ICourseRepository>>();
                mockedRepository.Setup(fake => fake.List())
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
        public async Task Get(long courseId)
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseController sut;
            Mock<ICourseRepository> mockedRepository;
            void Arrange()
            {
                mockedRepository = _fixture.Freeze<Mock<ICourseRepository>>();
                mockedRepository.Setup(fake => fake.Get(It.IsAny<long>()))
                                .Returns(Task.FromResult(_fixture.Build<Course>().With(course => course.CourseId, courseId).Create()));
                sut = _fixture.Create<CourseController>();
            }
            Task<ActionResult<CourseQueryDto>> resultTask;
            void Action()
            {
                resultTask = sut.Get(courseId);
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
    }
}
