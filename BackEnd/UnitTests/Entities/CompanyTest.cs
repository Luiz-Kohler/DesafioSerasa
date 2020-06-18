using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Entities
{
    public sealed class CompanyTest
    {
        [Test]
        public void FormatPropsShouldRemoveExtraWihteSpaces()
        {
            var company = new Company("  SERASA   BLUMENAU ");
            var expectedCompany = new Company("SERASA BLUMENAU");

            company.FormatProps();
            Assert.AreEqual(expectedCompany.Name, company.Name);
        }

        [Test]
        public void ShouldCalculateReliabilityWithDebitsAndInvoices()
        {
            var company = new Company("SERASA");
            var expectedCompany = 51;

            var invoice = new Invoice(0);
            var debit = new Debit(0);

            company.Debits.Add(debit);

            for (int i = 0; i < 3; i++)
            {
                company.Invoices.Add(invoice);
            }

            company.CalculateReliability();

            Assert.AreEqual(expectedCompany, company.Reliability);
        }

        [Test]
        public void ShouldReturnInvalid()
        {
            var company = new Company("");

            var expectedValue = false;
            var value = company.IsValid();

            Assert.AreEqual(value, expectedValue);
        }

        [Test]
        public void ShouldReturnHashSetMustBeInformed()
        {
            var company = new Company("");

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("The company name must be informed");

            var errors = company.GetErrors();

            errors.Should().BeEquivalentTo(expectedErrors);
        }

        [Test]
        public void ShouldReturnHashSetLenght()
        {
            var company = new Company("A");

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("The company name length must have between 2 and 50 characters");

            var errors = company.GetErrors();

            errors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
