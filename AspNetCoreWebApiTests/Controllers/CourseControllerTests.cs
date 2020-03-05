using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
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
        public void GetAll()
        {
            Arrange();
            Action();
            Asserts();

            CourseController sut;
            void Arrange()
            {
                sut = _fixture.Create<CourseController>();
            }
            IEnumerable<Course> result;
            void Action()
            {
                result = sut.Get();
            }
            void Asserts()
            {
                Assert.NotNull(result);
            }
        }

        [Theory, AutoData]
        public void Get(long courseId)
        {
            Arrange();
            Action();
            Asserts();

            CourseController sut;
            void Arrange()
            {
                sut = _fixture.Create<CourseController>();
            }
            Course result;
            void Action()
            {
                result = sut.Get(courseId);
            }
            void Asserts()
            {
                Assert.NotNull(result);
                Assert.Equal(courseId, result.CourseId);
            }
        }
    }
}
