using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using Xunit;

namespace AspNetCoreWebApiTests.Controller.Tests
{
    public class TuitionAgencyControllerTests
    {
        IFixture _fixture;
        
        public TuitionAgencyControllerTests() 
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
                assertions.Verify(typeof(TuitionAgencyController).GetConstructors());
            }
        } 

        [Fact]
        public void GetAll()
        {
            Arrange();
            Action();
            Asserts();

            TuitionAgencyController sut;
            void Arrange()
            {
                sut = _fixture.Create<TuitionAgencyController>();
            }
            IEnumerable<TuitionAgency> result;
            void Action()
            {
                result = sut.Get();
            }
            void Asserts()
            {
                Assert.NotNull(result);
            }
        }
    }
}