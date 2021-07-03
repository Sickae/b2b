using B2B.Shared.Interfaces;

namespace B2B.Shared.Dto
{
    public abstract class DtoBase : IDto
    {
        public int Id { get; set; }
    }
}
