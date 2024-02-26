namespace UsersAPI.Infra.Messages.Models;

public class MessagesRequestModel
{
    public string? MailTo { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public bool IsBodyHtml { get; set; }
}