﻿using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AspNetCoreWebApiTests.Controllers
{
    public class StudentControllerTests
    {
        IFixture _fixture;
        public StudentControllerTests()
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
                assertions.Verify(typeof(StudentController).GetConstructors());
            }
        }

        [Fact]
        public void GetAll()
        {
            Arrange();
            Action();
            Asserts();

            StudentController sut;
            void Arrange()
            {
                sut = _fixture.Create<StudentController>();
            }
            ActionResult<IEnumerable<Student>> result;
            void Action()
            {
                result = sut.Get();
            }
            void Asserts()
            {
                Assert.NotNull(result);
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var students = Assert.IsAssignableFrom<IEnumerable<Student>>(okResult.Value);
            }
        }

        [Theory, AutoData]
        public void Get(long studentId)
        {
            Arrange();
            Action();
            Asserts();

            StudentController sut;
            void Arrange()
            {
                sut = _fixture.Create<StudentController>();
            }
            ActionResult<Student> result;
            void Action()
            {
                result = sut.Get(studentId);
            }
            void Asserts()
            {
                Assert.NotNull(result);
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var student = Assert.IsType<Student>(okResult.Value);
                Assert.Equal(studentId, student.StudentId);
            }
        }
    }
}
