using System.Threading.Tasks;

namespace SignalR.Core.Web
{
    public interface ILearningHubClient
    {
        Task ReceiveMessage(string message);
    }
}
