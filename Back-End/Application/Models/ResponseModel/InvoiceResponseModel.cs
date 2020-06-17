namespace Application.Models.ResponseModel
{
    public class InvoiceResponseModel
    {
        public int Id { get; private set; }
        public virtual CompanyResponseModel Company { get; private set; }
    }
}
