using EventBus;
using MassTransit;

namespace report.application.Consumers
{
    public class PersonsConsumer : IConsumer<PersonQueue>
    { 
        public async Task Consume(ConsumeContext<PersonQueue> context)
        {
            var request = (PersonQueue)context.Message;
 
        }
    }
}
