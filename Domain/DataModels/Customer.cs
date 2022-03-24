namespace Domain.DataModels
{
    public class Customer : IntEntity
    {
        public virtual Account Account { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}