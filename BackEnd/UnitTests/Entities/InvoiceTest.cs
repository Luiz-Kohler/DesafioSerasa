using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Entities
{
    public sealed class InvoiceTest
    {
        [Test]
        public void ShouldReturnInvalid()
        {
            var invoice = new Invoice(-1);

            var expectedValue = false;
            var value = invoice.IsValid();

            Assert.AreEqual(value, expectedValue);
        }

        [Test]
        public void ShouldReturnHashSet()
        {
            var invoice = new Invoice(-1);

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("The company Id is invalid");

            var errors = invoice.GetErrors();

            errors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
