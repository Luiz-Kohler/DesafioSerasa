using Application.AutoMap;
using Application.IServices;
using Application.Models.RequestModel;
using Application.Models.ResponseModel;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Exceptions;
using Xunit;

namespace UnitTests.Application
{
    public sealed class DebitServiceTest
    {
        private readonly IDebitService _debitService;
        private readonly IDebitRepository _debitRepository;
        private readonly ICompanyRepository _companyRepository;

        public DebitServiceTest()
        {
            _debitService = Substitute.For<IDebitService>();
            _debitRepository = Substitute.For<IDebitRepository>();
            _companyRepository = Substitute.For<ICompanyRepository>();

            _debitService = new DebitService(_debitRepository, _companyRepository);
        }
        [Test]
        public async Task ShouldCreateDebit()
        {


            var expectedDebit = new Debit(1);
            var debitRequest = new DebitRequestModel()
            {
                CompanyId = 1,
            };

            var company = new Company("SERASA");

            _companyRepository.GetById(1).Returns(company);

            var response = await _debitService.Create(debitRequest);

            await _companyRepository
                .Received(1)
                    .GetById(Arg.Is<int>(x =>
                    x == 1));
            await _debitRepository
                 .Received(1)
                 .Create(Arg.Is<Debit>(x => x.CompanyId == 1));

            expectedDebit.Should().BeEquivalentTo(response);
        }
        [Test]
        public async Task ShouldReturnErrorAtCreateDebit()
        {
            var debitRequest = new DebitRequestModel()
            {
                CompanyId = -1,
            };

            var company = new Company("SERASA");

            _companyRepository.GetById(1).Returns(company);

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("The company Id is invalid");

            var ex = await Record.ExceptionAsync(() => _debitService.Create(debitRequest)) as BadRequestException;

            expectedErrors.Should().BeEquivalentTo(ex.Errors);
        }

        [Test]
        public async Task ShouldReturnErrorBecausseNotFoundCompanyAtCreateDebit()
        {
            var debitRequest = new DebitRequestModel()
            {
                CompanyId = 1,
            };

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("Company not found");

            var ex = await Record.ExceptionAsync(() => _debitService.Create(debitRequest)) as BadRequestException;

            expectedErrors.Should().BeEquivalentTo(ex.Errors);
        }

        [Test]
        public async Task ShouldGetDebitById()
        {


            var debit = new Debit(1);

            var expectedDebit = DebitMap.DebitToDebitResponse(debit);

            _debitRepository.GetById(1).Returns(debit);

            var response = await _debitService.GetById(1);

            response.Should().BeEquivalentTo(expectedDebit);
        }

        [Test]
        public async Task ShouldReturnErrorGetDebitById()
        {
            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("Id invalid");

            var ex = await Record.ExceptionAsync(() => _debitService.GetById(-1)) as BadRequestException;

            expectedErrors.Should().BeEquivalentTo(ex.Errors);
        }

        [Test]
        public async Task ShouldReturnErrorBecauseNotFoundGetDebitById()
        {
            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("Debit not found");

            var ex = await Record.ExceptionAsync(() => _debitService.GetById(1)) as BadRequestException;

            expectedErrors.Should().BeEquivalentTo(ex.Errors);
        }

        [Test]
        public async Task GetCompanyDebits()
        {
            var date = DateTime.UtcNow;

            var debitsOfCompanyModels = new List<DebitResponseModel>();
            var debitsOfCompany = new List<Debit>();

            var debitOfCompany1 = new Debit(1);

            var debitResponseOfCompany1 = DebitMap.DebitToDebitResponse(debitOfCompany1);

            for (int i = 0; i < 5; i++)
            {
                debitsOfCompanyModels.Add(debitResponseOfCompany1);
                debitsOfCompany.Add(debitOfCompany1);
            }

            _debitRepository.GetCompanyDebits(1).Returns(debitsOfCompany);

            var response = await _debitService.GetCompanyDebits(1);

            response.Should().BeEquivalentTo(debitsOfCompanyModels);
        }
    }
}
