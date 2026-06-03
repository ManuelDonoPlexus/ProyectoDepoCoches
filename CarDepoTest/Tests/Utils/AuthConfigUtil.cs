namespace CarDepoTest.Tests.Utils;

public class AuthConfigUtil
{
    public IConfiguration config;

    public string ReturnConfig()
    {
        return config["JWT:key"];
    }
}