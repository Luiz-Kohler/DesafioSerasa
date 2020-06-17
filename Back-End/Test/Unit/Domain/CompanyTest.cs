using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Test.Unit.Domain
{
    public sealed class CompanyTest
    {
        [Fact]
        public void FormatPropsShouldRemoveExtraWihteSpaces()
        {
            var company = new Company("  SERASA   BLUMENAU ");
            var expectedCompany = new Company("SERSASA BLUMENAU");

            company.FormatProps();

            expectedCompany.Name.Should().BeEquivalentTo(company.Name);
        }
    }
}
