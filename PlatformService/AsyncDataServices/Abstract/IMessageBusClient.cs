using PlatformService.Dtos;

namespace PlatformService.AsyncDataServices.Abstract
{
    public interface IMessageBusClient
    {
         void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
    }
}