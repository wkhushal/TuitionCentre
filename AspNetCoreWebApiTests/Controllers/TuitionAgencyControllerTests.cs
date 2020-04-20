using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi.DTOs.Query;
using AspNetCoreWebApi.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreWebApiTests.Controllers
{
    public class TuitionAgencyControllerTests
    {
        private readonly IFixture _fixture;
        
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
        public async Task GetAll()
        {
            Arrange();
            Action();
            await Asserts().ConfigureAwait(false);

            TuitionAgencyController sut;
            void Arrange()
            {
                sut = _fixture.Create<TuitionAgencyController>();
            }
            Task<ActionResult<IEnumerable<TuitionAgencyQueryDto>>> actionResultTask;
            void Action()
            {
                actionResultTask = sut.Get();
            }
            async Task Asserts()
            {
                var actionResult = await actionResultTask.ConfigureAwait(false);
                Assert.NotNull(actionResult);
                Assert.IsType<NoContentResult>(actionResult.Result);
            }
        }
    }
}