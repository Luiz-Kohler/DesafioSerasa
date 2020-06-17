using Application.IServices;
using Application.Models.RequestModel;
using Application.Models.ResponseModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Utils.Exceptions;
using Web.Controllers;

namespace UnitTests.Controller
{
    public class CompanyControllerTest
    {
        private readonly ICompanyService _companyService;
        private readonly CompanyController _controller;

        public CompanyControllerTest()
        {
            _companyService = Substitute.For<ICompanyService>();
            _controller = new CompanyController(_companyService);
        }

        [Test]
        public async Task ShouldCreateCompany()
        {
            var companyRequest = new CompanyRequestModel()
            {
                Name = "SERASA"
            };
            var companyResponse = new CompanyResponseModel(0, "SERASA", 50);

            _companyService.Create(companyRequest).Returns(companyResponse);

            var responseController = await _controller.Create(companyRequest) as ObjectResult;
            var response = responseController.Value as CompanyResponseModel;

            await _companyService.Received(1)
                .Create(Arg.Is<CompanyRequestModel>
                (x => x.Name == "SERASA"));

            Assert.AreEqual((int)HttpStatusCode.Created, responseController.StatusCode);
            companyResponse.Should().BeEquivalentTo(response);
        }

        [Test]
        public async Task ShouldReturnErrorAtCreateCompany()
        {
            var companyRequest = new CompanyRequestModel()
            {
                Name = ""
            };

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("The company name length must have between 2 and 50 characters");

            _companyService.Create(companyRequest).Throws(new BadRequestException(expectedErrors));

            var responseController = await _controller.Create(companyRequest) as BadRequestObjectResult;
            var responseErrors = responseController.Value as HashSet<string>;

            await _companyService.Received(1)
                .Create(Arg.Is<CompanyRequestModel>
                (x => x.Name == ""));

            Assert.AreEqual((int)HttpStatusCode.BadRequest, responseController.StatusCode);
            expectedErrors.Should().BeEquivalentTo(responseErrors);
        }

        [Test]
        public async Task ShouldGetCompanyById()
        {
            var id = 1;
            var expectedCompanyResponse = new CompanyResponseModel(0, "SERASA", 50);

            _companyService.GetById(id).Returns(expectedCompanyResponse);

            var responseController = await _controller.GetById(id) as OkObjectResult;
            var responseCompany = responseController.Value as CompanyResponseModel;

            await _companyService.Received(1)
                .GetById(Arg.Is<int>
                (x => x == id));

            Assert.AreEqual((int)HttpStatusCode.OK, responseController.StatusCode);
            expectedCompanyResponse.Should().BeEquivalentTo(responseCompany);
        }

        [Test]
        public async Task ShouldReturnErrorAtGetCompanyById()
        {
            var id = -1;
            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("Id invalid");

            _companyService.GetById(id).Throws(new BadRequestException(expectedErrors));

            var responseController = await _controller.GetById(id) as BadRequestObjectResult;
            var responseErrors = responseController.Value as HashSet<string>;

            await _companyService.Received(1)
                .GetById(Arg.Is<int>
                (x => x == id));

            Assert.AreEqual((int)HttpStatusCode.BadRequest, responseController.StatusCode);
            expectedErrors.Should().BeEquivalentTo(responseErrors);
        }

        [Test]
        public async Task ShouldGetOrderByDescending()
        {
            var expectedCompanies = new List<CompanyResponseModel>();

            var badCompany = new CompanyResponseModel(1, "AAA", 20);
            var goodCompany = new CompanyResponseModel(2, "BBB", 50);
            var niceCompany = new CompanyResponseModel(3, "CCC", 50);

            expectedCompanies.Add(badCompany);
            expectedCompanies.Add(goodCompany);
            expectedCompanies.Add(niceCompany);

            _companyService.GetOrderByDescending(1).Returns(expectedCompanies);

            var responseController = await _controller.GetOrderByDescending(1) as OkObjectResult;
            var responseCompanies = responseController.Value as List<CompanyResponseModel>;

            await _companyService.Received(1)
                .GetOrderByDescending(Arg.Is<int>
                (x => x == 1));

            Assert.AreEqual((int)HttpStatusCode.OK, responseController.StatusCode);
            expectedCompanies.Should().BeEquivalentTo(responseCompanies);
        }

        [Test]
        public async Task ShouldGetOrderByCrescent()
        {
            var expectedCompanies = new List<CompanyResponseModel>();

            var badCompany = new CompanyResponseModel(1, "AAA", 20);
            var goodCompany = new CompanyResponseModel(2, "BBB", 50);
            var niceCompany = new CompanyResponseModel(3, "CCC", 50);

            expectedCompanies.Add(badCompany);
            expectedCompanies.Add(goodCompany);
            expectedCompanies.Add(niceCompany);

            _companyService.GetOrderByCrescent(1).Returns(expectedCompanies);

            var responseController = await _controller.GetOrderByCrescent(1) as OkObjectResult;
            var responseCompanies = responseController.Value as List<CompanyResponseModel>;

            await _companyService.Received(1)
                .GetOrderByCrescent(Arg.Is<int>
                (x => x == 1));

            Assert.AreEqual((int)HttpStatusCode.OK, responseController.StatusCode);
            expectedCompanies.Should().BeEquivalentTo(responseCompanies);
        }
    }
}
