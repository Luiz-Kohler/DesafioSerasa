using Application.AutoMap;
using Application.IServices;
using Application.Models.RequestModel;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Exceptions;
using Xunit;

namespace UnitTests.Application
{
    public sealed class CompanyServiceTest
    {
        private readonly ICompanyRepository _repository;
        private readonly ICompanyService _service;

        public CompanyServiceTest()
        {
            _repository = Substitute.For<ICompanyRepository>();
            _service = Substitute.For<ICompanyService>();

            _service = new CompanyService(_repository);
        }

        [Test]
        public async Task ShouldCreateCompany()
        {
            var expectedCompany = new Company("SERASA");
            var companyRequest = new CompanyRequestModel()
            {
                Name = "SERASA",
            };

            _repository.CompanyExists(expectedCompany.Name).Returns(false);

            var response = await _service.Create(companyRequest);

            await _repository
                .Received(1)
                    .Create(Arg.Is<Company>(x =>
                    x.Name == "SERASA"));

            await _repository
                .Received(1)
                    .CompanyExists(Arg.Is<string>(x =>
                    x == "SERASA"));

            expectedCompany.Should().BeEquivalentTo(response);
        }

        [Test]
        public async Task ShouldReturnErrorMustBeInformedAtCreateCompany()
        {
            var companyRequest = new CompanyRequestModel()
            {
                Name = "S",
            };

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("The company name length must have between 2 and 50 characters");

            _repository.CompanyExists(companyRequest.Name).Returns(false);

            var ex = await Record.ExceptionAsync(() => _service.Create(companyRequest)) as BadRequestException;

            ex.Errors.Should().BeEquivalentTo(expectedErrors);
        }

        [Test]
        public async Task ShouldReturnErrorUniqueValeuConstraintAtCreateCompany()
        {
            var companyRequest = new CompanyRequestModel()
            {
                Name = "SERASA",
            };

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("This company name already exists");

            _repository.CompanyExists("SERASA").Returns(true);

            var ex = await Record.ExceptionAsync(() => _service.Create(companyRequest)) as BadRequestException;

            await _repository
                .Received(1)
                    .CompanyExists(Arg.Is<string>(x =>
                    x == "SERASA"));

            ex.Errors.Should().BeEquivalentTo(expectedErrors);
        }

        [Test]
        public async Task ShouldGetCompanyById()
        {
            var company = new Company("SERASA");
            var expectedCompanyResponse = CompanyMap.CompanyToCompanyResponse(company);

            _repository.GetById(1).Returns(company);

            var response = await _service.GetById(1);

            expectedCompanyResponse.Should().BeEquivalentTo(response);
        }

        [Test]
        public async Task ShouldReturnErrorAtGetGetCompanyById()
        {
            var company = new Company("SERASA");
            var expectedCompanyResponse = CompanyMap.CompanyToCompanyResponse(company);

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("Id invalid");

            var ex = await Record.ExceptionAsync(() => _service.GetById(-1)) as BadRequestException;

            ex.Errors.Should().BeEquivalentTo(expectedErrors);
        }

        [Test]
        public async Task ShouldReturnErrorBecauseNotFoundCompanyAtGetGetCompanyById()
        {
            var company = new Company("SERASA");
            var expectedCompanyResponse = CompanyMap.CompanyToCompanyResponse(company);

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("Company not found");

            var ex = await Record.ExceptionAsync(() => _service.GetById(1)) as BadRequestException;

            ex.Errors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
