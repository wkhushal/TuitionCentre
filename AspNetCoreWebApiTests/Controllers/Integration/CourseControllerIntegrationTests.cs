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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreWebApiTests.Controllers.Integration
{
    public class CourseControllerIntegrationTests
    {
        IFixture _fixture;
        public CourseControllerIntegrationTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            _fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
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
                mockedRepository.Setup(fake => fake.List());

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
                mockedRepository.Verify(fake => fake.List(), Times.Once);
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
                mockedRepository.Setup(fake => fake.Get(It.IsAny<long>()));
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
                mockedRepository.Verify(fake => fake.Get(It.IsAny<long>()), Times.Once);
            }
        }

        [Theory, AutoData]
        public async Task Update(long courseId, CourseDto update)
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            CourseController sut;
            Mock<ICourseRepository> mockedRepository;
            void Arrange()
            {
                mockedRepository = _fixture.Freeze<Mock<ICourseRepository>>();
                mockedRepository.Setup(fake => fake.Update(It.IsAny<long>(), It.IsAny<Course>()));
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
                mockedRepository.Verify(fake => fake.Update(It.IsAny<long>(), It.IsAny<Course>()), Times.Once);
            }
        }
    }
}
