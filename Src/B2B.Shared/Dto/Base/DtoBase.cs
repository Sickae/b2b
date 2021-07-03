using B2B.Shared.Interfaces;

namespace B2B.Shared.Dto.Base
{
    public abstract class DtoBase : IDto
    {
        public int Id { get; set; }
    }
}
