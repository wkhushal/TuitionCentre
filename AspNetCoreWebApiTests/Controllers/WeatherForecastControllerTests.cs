using AspNetCoreWebApi.Controllers;
using AspNetCoreWebApi;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApiTests.Controllers
{
    public class WeatherForecastControllerTests
    {
        IFixture _fixture;

        public WeatherForecastControllerTests()
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
                assertions.Verify(typeof(WeatherForecastController).GetConstructors());
            }
        }

        [Fact]
        public void GetAll()
        {
            Arrange();
            Action();
            Asserts();

            WeatherForecastController sut;
            void Arrange()
            {
                sut = _fixture.Create<WeatherForecastController>();
            }
            ActionResult<IEnumerable<WeatherForecast>> result;
            void Action()
            {
                result = sut.Get();
            }
            void Asserts()
            {
                Assert.NotNull(result);
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(okResult.Value);
            }
        }
    }
}
