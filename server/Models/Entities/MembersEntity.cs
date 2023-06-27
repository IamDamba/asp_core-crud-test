namespace server.Models.Entities;

public class MembersEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Pwd { get; set; }
    public DateTime CreatedAt { get; set; }

    public static string HashPwd(string _pwd)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hash = BCrypt.Net.BCrypt.HashPassword(_pwd, salt);

        return hash;
    }

    public static bool VerifyPwd(string _pwd, string db_pwd)
    {
        var verify = BCrypt.Net.BCrypt.Verify(_pwd, db_pwd);

        return verify;
    }
}