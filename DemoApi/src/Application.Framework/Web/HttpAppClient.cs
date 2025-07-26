using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Application.Framework;

public class HttpAppClient : BaseObject
{
    protected HttpClient _httpClient = null;

    public HttpAppClient(HttpClient httpClient)
    {
        _info.Add("Application", "Application.Framework");
        _info.Add("Module", "HttpAppClient");

        _httpClient = httpClient;
        _info.Add("httpClient.BaseAddress", _httpClient.BaseAddress);
    }

    protected async Task<TEntity> ProcessSuccessResponseAsync<TEntity>(HttpResponseMessage response, AdditionalInfo info)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            var responseValue = JsonConvert.DeserializeObject<ApiResponse<TEntity>>(responseContent);
            if (responseValue != null && responseValue.GetType().ToString().Contains("ApiResponse"))
            {
                foreach (var property in responseValue.GetType().GetProperties())
                {
                    var propertyValue = property.GetValue(responseValue);
                    if (property?.Name == "RuleViolation")
                    {
                        info = propertyValue != null ? (AdditionalInfo)propertyValue : new AdditionalInfo();
                    }
                }
                if (info != null && info.Count > 0)
                {
                    RuleViolation.Add(info.Value);
                }
                if (responseValue.Data != null)
                {
                    return (TEntity)responseValue.Data;
                }
            }
        }
        catch (Exception ex)
        {
            info.Add("RequestUri", response?.RequestMessage?.RequestUri);
            info.Add("StatusCode", response?.StatusCode);
            info.Add("ReasonPhrase", response?.ReasonPhrase);
            info.Add("Content", responseContent);
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return (default);
    }

    protected async Task ProcessUnSuccessResponseAsync(HttpResponseMessage response, AdditionalInfo info)
    {
        var message = string.Empty;

        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        info.Add("RequestUri", response?.RequestMessage?.RequestUri);
        info.Add("StatusCode", response?.StatusCode);
        info.Add("ReasonPhrase", response?.ReasonPhrase);
        if (AppUtility.IsValidJson(responseContent))
        {   
            var messageList = new AdditionalInfo(responseContent);
            if (message != null && messageList.Count > 0)
            {
                message = messageList.GetStringValue("Message", true);
                if (string.IsNullOrEmpty(message))
                {
                    message = messageList.GetStringValue("error_description", true);
                }
                info.Add(messageList.Value);
            }
        }
        var ex = new Exception(message);
        info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
        throw ex;
    }

    public async Task<bool> FindApiEntityAsync(string requestUri, CancellationToken cancellationToken = default)
    {
        AdditionalInfo info = new(_info.Value);
        info.Add("Method", "FindApiEntityAsync");
        info.Add("Message", $"FindApiEntityAsync(string requestUri: {requestUri}, CancellationToken cancellationToken = default");

        ClearViolation();
        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode) return (await ProcessSuccessResponseAsync<bool>(response, info).ConfigureAwait(false));
            await ProcessUnSuccessResponseAsync(response, info).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return (false);
    }

    public virtual async Task<TEntity> GetApiEntityAsync<TEntity>(string requestUri, CancellationToken cancellationToken = default)
    {
        AdditionalInfo info = new(_info.Value);
        info.Add("Method", "GetApiEntityAsync");
        info.Add("Message", $"GetApiEntityAsync<TEntity>(string requestUri: {requestUri}, CancellationToken cancellationToken = default)");

        ClearViolation();
        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode) return (await ProcessSuccessResponseAsync<TEntity>(response, info).ConfigureAwait(false));
            await ProcessUnSuccessResponseAsync(response, info).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return (default);
    }

    public async Task<TEntity> PostApiSearchCriteriaAsync<TEntity>(string requestUri, SearchCriteria searchCriteria, CancellationToken cancellationToken = default)
    {
         return (await PostApiHttpContentAsync<TEntity>(requestUri, new StringContent(JsonConvert.SerializeObject(searchCriteria), System.Text.Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false));
    }

    public async Task<TEntity> PostApiEntityAsync<TEntity>(string requestUri, TEntity model, CancellationToken cancellationToken = default)
    {
         return (await PostApiHttpContentAsync<TEntity>(requestUri, new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false));
    }

    public virtual async Task<TEntity> PostApiHttpContentAsync<TEntity>(string requestUri, HttpContent content, CancellationToken cancellationToken = default)
    {
        AdditionalInfo info = new(_info.Value);
        info.Add("Method", "PostApiHttpContentAsync");
        info.Add("Message", $"PostApiEntityAsync<TEntity>(string requestUri: {requestUri}, HttpContent content, CancellationToken cancellationToken = default)");

        ClearViolation();
        try
        {
            using HttpResponseMessage response = await _httpClient.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode) return (await ProcessSuccessResponseAsync<TEntity>(response, info).ConfigureAwait(false));
            await ProcessUnSuccessResponseAsync(response, info).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return (default);
    }

    public async Task<TEntity> PutApiEntityAsync<TEntity>(string requestUri, TEntity model, CancellationToken cancellationToken = default)
    {
        return (await PutApiHttpContentAsync<TEntity>(requestUri, new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false));
    }

    public async Task<TEntity> PutApiHttpContentAsync<TEntity>(string requestUri, HttpContent content, CancellationToken cancellationToken = default)
    {
        AdditionalInfo info = new(_info.Value);
        info.Add("Method", "PutApiHttpContentAsync");
        info.Add("Message", $"PutApiHttpContentAsync<TEntity>(string requestUri: {requestUri}, HttpContent content, CancellationToken cancellationToken = default)");

        ClearViolation();
        try
        {
            using HttpResponseMessage response = await _httpClient.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode) return (await ProcessSuccessResponseAsync<TEntity>(response, info).ConfigureAwait(false));
            await ProcessUnSuccessResponseAsync(response, info).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return (default);
    }

    public async Task<bool> DeleteApiEntityAsync(string requestUri, CancellationToken cancellationToken = default)
    {
        AdditionalInfo info = new(_info.Value);
        info.Add("Method", "DeleteApiEntityAsync");
        info.Add("Message", $"DeleteApiEntityAsync(string requestUri: {requestUri}, CancellationToken cancellationToken = default)");

        ClearViolation();
        try
        {
            using HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode) return (true);
            await ProcessUnSuccessResponseAsync(response, info).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return (false);
    }

    public async Task<bool> DeleteApiEntityAsync<TEntity>(string requestUri, TEntity model, CancellationToken cancellationToken = default)
    {
        AdditionalInfo info = new(_info.Value);
        info.Add("Method", "DeleteApiEntityAsync");
        info.Add("Message", $"DeleteApiEntityAsync<TEntity>(string requestUri: {requestUri}, TEntity model, CancellationToken cancellationToken = default)");

        ClearViolation();
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode) return (true);
            await ProcessUnSuccessResponseAsync(response, info).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return (false);
    }

    public virtual async Task<TEntity> SendApiContentAsync<TEntity>(string requestUri, HttpMethod method, HttpContent content, CancellationToken cancellationToken = default)
    {
        AdditionalInfo info = new(_info.Value);
        info.Add("Method", "SendApiHttpContentAsync");
        info.Add("Message", $"SendApiContentAsync<TEntity>(string requestUri: {requestUri}, HttpMethod method: {method}, HttpContent content, CancellationToken cancellationToken = default)");

        ClearViolation();
        try
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(requestUri),
                Content = content
            };
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode) return (await ProcessSuccessResponseAsync<TEntity>(response, info).ConfigureAwait(false));
            await ProcessUnSuccessResponseAsync(response, info).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                info.Add("InnerException", ex.InnerException.Message);
            }
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return (default);
    }

}
