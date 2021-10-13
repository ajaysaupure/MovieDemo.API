using MovieDemo.Entity.Interfaces;

namespace MovieDemo.Entity.BusinessEntities
{
    public class UserEntity : IBusinessEntity
    {
        public long UserId { get; set; }
        public string Name { get; set; }
    }
}
