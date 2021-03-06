﻿using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi.DTOs.Query;
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
    public class SubjectControllerTests
    {
        private readonly IFixture _fixture;
        public SubjectControllerTests()
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
                assertions.Verify(typeof(SubjectController).GetConstructors());
            }
        }

        [Fact]
        public void GetAll()
        {
            Arrange();
            Action();
            Asserts();

            SubjectController sut;
            void Arrange()
            {
                sut = _fixture.Create<SubjectController>();
            }
            ActionResult<IEnumerable<SubjectQueryDto>> result;
            void Action()
            {
                result = sut.Get();
            }
            void Asserts()
            {
                Assert.NotNull(result);
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                Assert.IsAssignableFrom<IEnumerable<SubjectQueryDto>>(okResult.Value);
            }
        }

        [Theory, AutoData]
        public void GetById(long subjectId)
        {
            Arrange();
            Action();
            Asserts();

            SubjectController sut;
            void Arrange()
            {
                sut = _fixture.Create<SubjectController>();
            }
            ActionResult<SubjectQueryDto> result;
            void Action()
            {
                result = sut.GetById(subjectId);
            }
            void Asserts()
            {
                Assert.NotNull(result);
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var subject = Assert.IsType<SubjectQueryDto>(okResult.Value);
                Assert.Equal(subjectId, subject.SubjectId);
            }
        }
    }
}
