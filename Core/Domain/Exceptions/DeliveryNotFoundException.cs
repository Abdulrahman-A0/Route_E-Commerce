namespace Domain.Exceptions
{
    public class DeliveryNotFoundException : NotFoundException
    {
        public DeliveryNotFoundException(int id) : base($"Delivery Method With id: {id} Not Found")
        {
        }
    }
}
