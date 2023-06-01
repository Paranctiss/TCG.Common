namespace TCG.Common.Settings;

public class KeycloakSetting
{
    public string Audience { get; set; }
    public string Realm { get; set; }
    public string BaseUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }

    public string BaseRealmUrl => $"{BaseUrl}/realms/{Realm}";
    public string TokenEndpoint => $"{BaseRealmUrl}/protocol/openid-connect/token";
    public string UserInfoEndpoint => $"{BaseRealmUrl}/protocol/openid-connect/userinfo";
    public string AdminUsersEndpoint => $"{BaseUrl}/admin/realms/{Realm}/users";
}