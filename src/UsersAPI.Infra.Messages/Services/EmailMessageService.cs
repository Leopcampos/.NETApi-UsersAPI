using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using UsersAPI.Infra.Messages.Models;
using UsersAPI.Infra.Messages.Settings;

namespace UsersAPI.Infra.Messages.Services;

public class EmailMessageService
{
    private readonly EmailMessageSettings? _emailMessageSettings;

    public EmailMessageService(IOptions<EmailMessageSettings>? emailMessageSettings)
        => _emailMessageSettings = emailMessageSettings?.Value;

    public async Task SendMessage(MessagesRequestModel messgaesRequestModel)
    {
        using (var httpClient = new HttpClient())
        {
            try
            {
                //Autenticando na API de mensagens
                var authResponseModel = await ExecuteAuth();
                
                //Criando uma parametro de cabeçalho com o token
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponseModel.Token);

                var messagesRequestContent = new StringContent(JsonConvert.SerializeObject(messgaesRequestModel),
                    Encoding.UTF8, "application/json");

                await httpClient.PostAsync($"{_emailMessageSettings?.BaseUrl}/messages", messagesRequestContent);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }

    private async Task<AuthResponseModel> ExecuteAuth()
    {
        using (var httpClient = new HttpClient())
        {
            var authRequestModel = new AuthRequestModel
            {
                Key = _emailMessageSettings?.User,
                Pass = _emailMessageSettings?.Pass
            };

            var authRequestContent = new StringContent(JsonConvert.SerializeObject(authRequestModel),
                Encoding.UTF8, "application/json");

            var authResponse = await httpClient.PostAsync($"{_emailMessageSettings?.BaseUrl}/auth", authRequestContent);
            return ReadResponse<AuthResponseModel>(authResponse);
        }
    }

    private T ReadResponse<T>(HttpResponseMessage response)
    {
        var builder = new StringBuilder();
        using (var item = response.Content)
        {
            var task = item.ReadAsStringAsync();
            builder.Append(task.Result);
        }

        return JsonConvert.DeserializeObject<T>(builder.ToString());
    }
}