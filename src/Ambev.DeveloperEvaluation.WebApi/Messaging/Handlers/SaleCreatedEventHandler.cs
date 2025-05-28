using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.WebApi.Messaging.Handlers;

public class SaleCreatedEventHandler : IHandleMessages<SaleCreatedEvent>
{
    public Task Handle(SaleCreatedEvent message)
    {
        Console.WriteLine($"🚀 SaleCreatedEvent received: {message.SaleId} for {message.Customer}");
        return Task.CompletedTask;
    }
}
