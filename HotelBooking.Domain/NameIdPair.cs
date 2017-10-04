namespace HotelBooking.Domain
{
    public class NameIdPair : IdentifiableObject
    {
        public string Name { get; }

        public NameIdPair(int id, string name): base(id)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}
