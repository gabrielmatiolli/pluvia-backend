namespace Pluvia.Domain.UseCases.Auth.Inputs;

public sealed class AuthTokenSettings
{
    public string Key { get; init; } = string.Empty;
    public string Issuer { get; init; } = "pluvia-api";
    public string Audience { get; init; } = "pluvia-client";
    public int ExpiresInMinutes { get; init; } = 60;
}
