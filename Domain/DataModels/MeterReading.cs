namespace Domain.DataModels
{
    public class MeterReading : IntEntity 
    {
        /// <summary>
        /// The account on which the meter reading is made against 
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// Gives the meter reading date time
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gives the metter reating value will have 5 decimal places
        /// </summary>
        public virtual string Value { get; set; }
    }
}