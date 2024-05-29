using System.Net;
using System.Text.Json;
using Application.Dtos.Validation.PhoneValidation;
using Application.Options;
using Application.Services.ValidationServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;

namespace UnitTests.Services;

public abstract class PhoneValidatorServiceTestsFixture
{
    protected const string PhoneNumber = "1234567890";
    private const string ApiKey = "test";
    private readonly Mock<ILogger<PhoneValidatorService>> Logger = new();
    private readonly Mock<IHttpClientFactory> HttpClientFactory = new();
    protected PhoneValidatorService PhoneValidatorService;


    protected PhoneValidatorServiceTestsFixture()
    {
        var phoneValidatorOptions = new PhoneValidatorOptions()
        {
            ApiKey = ApiKey
        };
        var options = Options.Create(phoneValidatorOptions);
        
        PhoneValidatorService = new PhoneValidatorService(Logger.Object, HttpClientFactory.Object, options);
    }
    private static MockHttpMessageHandler CreateMockHttpMessageHandler
        (HttpStatusCode statusCode, PhoneValidatorResponse response)
    {
        var handler = new MockHttpMessageHandler();

        handler
            .When(HttpMethod.Get, $"https://phonevalidation.abstractapi.com/v1/?api_key={ApiKey}&phone={PhoneNumber}")
            .Respond(statusCode, "application/json", JsonSerializer.Serialize(response));
        
        return handler;
    }
    
    protected void SetupHttpClientFactory(HttpStatusCode statusCode, bool valid = true, bool isNull = false)
    {
        
        var response = new PhoneValidatorResponse
        (PhoneNumber, true, new Format("", ""), new Country("", "", ""),
            "", "", "");
        
        if (isNull)
        {
            response = null;
        }
        
        if (!valid)
        {
            response = new PhoneValidatorResponse
            (PhoneNumber, false, new Format("", ""), new Country("", "", ""),
                "", "", "");
        }
        
        var httpClient = new HttpClient(CreateMockHttpMessageHandler(statusCode, response));
        HttpClientFactory
            .Setup(factory => factory.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);
    }
    
    protected void SetupHttpClientFactoryWithThrow()
    {
        var handler = new MockHttpMessageHandler();

        handler
            .When(HttpMethod.Get, $"https://phonevalidation.abstractapi.com/v1/?api_key={ApiKey}&phone={PhoneNumber}")
            .Throw(new Exception());

        var httpClient = new HttpClient(handler);
        
        HttpClientFactory
            .Setup(factory => factory.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);
    }
}