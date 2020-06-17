using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Entities
{
    public sealed class DebitTest
    {
        [Test]
        public void ShouldReturnInvalid()
        {
            var debit = new Debit(-1);

            var expectedValue = false;
            var value = debit.IsValid();

            Assert.AreEqual(value, expectedValue);
        }

        [Test]
        public void ShouldReturnHashSet()
        {
            var debit = new Debit(-1);

            var expectedErrors = new HashSet<string>();
            expectedErrors.Add("The Id of company must be greater than 0");

            var errors = debit.GetErrors();

            errors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
