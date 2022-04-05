namespace Djinni.Data.Entities
{
    public class Person
    {
        public long Id { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public long AddressId { get; set; }
        public virtual Address address { get; set; }

    }
}
