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
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Exceptions;
using Xunit;

namespace UnitTests.Application
{
    public sealed class InvoiceServiceTest
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICompanyRepository _companyRepository;

        public InvoiceServiceTest()
        {
            _invoiceService = Substitute.For<IInvoiceService>();
            _invoiceRepository = Substitute.For<IInvoiceRepository>();
            _companyRepository = Substitute.For<ICompanyRepository>();

            _invoiceService = new InvoiceService(_invoiceRepository, _companyRepository);
        }

        [Test]
        public async Task ShouldCreateInvoice()
        {


            var expectedInvoice = new Invoice(1);
            var InvoiceRequest = new InvoiceRequestModel()
            {
                CompanyId = 1,
            };

            var company = new Company("SERASA");

            _companyRepository.GetById(1).Returns(company);

            var response = await _invoiceService.Create(InvoiceRequest);

            await _companyRepository
                .Received(1)
                    .GetById(Arg.Is<int>(x =>
                    x == 1));
            await _invoiceRepository
                 .Received(1)
                 .Create(Arg.Is<Invoice>(x => x.CompanyId == 1));

            expectedInvoice.Should().BeEquivalentTo(response);
        }
        [Test]
        public async Task ShouldReturnErrorAtCreateInvoice()
        {
            var InvoiceRequest = new InvoiceRequestModel()
            {
                CompanyId = -1,
            };

            var company = new Company("SERASA");

            _companyRepository.GetById(1).Returns(company);

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("The company Id is invalid");

            var ex = await Record.ExceptionAsync(() => _invoiceService.Create(InvoiceRequest)) as BadRequestException;

            expectedErrors.Should().BeEquivalentTo(ex.Errors);
        }

        [Test]
        public async Task ShouldReturnErrorBecausseNotFoundCompanyAtCreateInvoice()
        {
            var InvoiceRequest = new InvoiceRequestModel()
            {
                CompanyId = 1,
            };

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("Company not found");

            var ex = await Record.ExceptionAsync(() => _invoiceService.Create(InvoiceRequest)) as BadRequestException;

            expectedErrors.Should().BeEquivalentTo(ex.Errors);
        }

        [Test]
        public async Task ShouldGetInvoiceById()
        {


            var Invoice = new Invoice(1);

            var expectedInvoice = InvoiceMap.InvoiceToInvoiceResponse(Invoice);

            _invoiceRepository.GetById(1).Returns(Invoice);

            var response = await _invoiceService.GetById(1);

            response.Should().BeEquivalentTo(expectedInvoice);
        }

        [Test]
        public async Task ShouldReturnErrorGetInvoiceById()
        {
            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("Id invalid");

            var ex = await Record.ExceptionAsync(() => _invoiceService.GetById(-1)) as BadRequestException;

            expectedErrors.Should().BeEquivalentTo(ex.Errors);
        }

        [Test]
        public async Task ShouldReturnErrorBecauseNotFoundGetInvoiceById()
        {
            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("Invoice not found");

            var ex = await Record.ExceptionAsync(() => _invoiceService.GetById(1)) as BadRequestException;

            expectedErrors.Should().BeEquivalentTo(ex.Errors);
        }

        [Test]
        public async Task GetCompanyInvoices()
        {
            var InvoicesOfCompanyModels = new List<InvoiceResponseModel>();
            var InvoicesOfCompany = new List<Invoice>();

            var InvoiceOfCompany1 = new Invoice(1);

            var InvoiceResponseOfCompany1 = InvoiceMap.InvoiceToInvoiceResponse(InvoiceOfCompany1);

            for (int i = 0; i < 5; i++)
            {
                InvoicesOfCompanyModels.Add(InvoiceResponseOfCompany1);
                InvoicesOfCompany.Add(InvoiceOfCompany1);
            }

            _invoiceRepository.GetCompanyInvoices(1).Returns(InvoicesOfCompany);

            var response = await _invoiceService.GetCompanyInvoices(1);

            response.Should().BeEquivalentTo(InvoicesOfCompanyModels);
        }
    }
}
