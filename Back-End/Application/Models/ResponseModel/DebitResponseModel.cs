
namespace Application.Models.ResponseModel
{
    public class DebitResponseModel
    {
        public int Id { get; private set; }
        public virtual CompanyResponseModel Company { get; private set; }
    }
}
