namespace HotelBooking.Domain
{
    public class IdentifiableObject
    {
        public int Id { get; }

        public IdentifiableObject(int id)
        {
            Id = id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as IdentifiableObject;
            return Id == other?.Id;
        }
    }
}
