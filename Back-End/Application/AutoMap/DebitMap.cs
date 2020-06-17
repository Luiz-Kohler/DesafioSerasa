using Application.Models.RequestModel;
using Application.Models.ResponseModel;
using Domain.Entities;
using Utils;

namespace Application.AutoMap
{
    public static class DebitMap
    {
        public static Debit DebitRequestToDebit(DebitRequestModel debitRequest)
        {
            return AutoMapperFunc.ChangeValues<DebitRequestModel, Debit>(debitRequest);
        }

        public static DebitResponseModel DebitToDebitResponse(Debit debit)
        {
            return AutoMapperFunc.ChangeValues<Debit, DebitResponseModel>(debit);
        }
    }
}
