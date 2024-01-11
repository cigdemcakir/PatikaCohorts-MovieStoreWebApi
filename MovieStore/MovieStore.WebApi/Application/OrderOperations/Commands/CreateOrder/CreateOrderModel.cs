namespace MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
    }
}
