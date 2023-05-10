namespace CommentService.EventProcessing.PostProcessors
{
    public interface IAddPostEventProcessor
    {
        Task ProcessAddPostEvent(string message);
    }
}
