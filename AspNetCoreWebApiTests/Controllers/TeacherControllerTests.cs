using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AspNetCoreWebApiTests.Controllers
{
    public class TeacherControllerTests
    {
        IFixture _fixture;

        public TeacherControllerTests()
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
                assertions.Verify(typeof(TeacherController).GetConstructors());
            }
        }

        [Fact]
        public void GetAll()
        {
            Arrange();
            Action();
            Asserts();

            TeacherController sut;
            void Arrange()
            {
                sut = _fixture.Create<TeacherController>();
            }
            ActionResult<IEnumerable<Teacher>> result;
            void Action()
            {
                result = sut.Get();
            }
            void Asserts()
            {
                Assert.NotNull(result);
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                Assert.IsAssignableFrom<IEnumerable<Teacher>>(okResult.Value);
            }
        }
    }
}
