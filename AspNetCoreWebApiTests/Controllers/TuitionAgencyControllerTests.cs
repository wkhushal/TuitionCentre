using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using Xunit;

namespace AspNetCoreWebApiTests.Controllers
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
            ActionResult<IEnumerable<TuitionAgency>> actionResult;
            void Action()
            {
                actionResult = sut.Get();
            }
            void Asserts()
            {
                Assert.NotNull(actionResult);
                Assert.IsType<OkObjectResult>(actionResult.Result);
            }
        }
    }
}