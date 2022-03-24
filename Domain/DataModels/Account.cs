namespace Domain.DataModels
{
    public class Account : IntEntity
    {
        public virtual IList<Customer> Customers { get; set; }

        public virtual IList<MeterReading>? MeterReadings { get; set; }
    }
}