using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Fortes.Security
{
  /// <summary>
  /// 
  /// </summary>
  public class JwtTokenOptions
  {
    /// <summary>
    /// 
    /// </summary>
    public readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("633d8c82a4a72d2fb9670eff47aa73fe"));

    /// <summary>
    /// 
    /// </summary>
    public string Issuer { get; set; } = "Issuer";

    /// <summary>
    /// 
    /// </summary>
    public string Audience { get; set; } = "Audience";

    /// <summary>
    /// 
    /// </summary>
    public DateTime NotBefore => DateTime.Now;

    /// <summary>
    /// 
    /// </summary>
    public DateTime IssuedAt => DateTime.Now;

    /// <summary>
    /// 
    /// </summary>
    public DateTime Expiration => DateTime.Now.AddYears(1);

    /// <summary>
    /// 
    /// </summary>
    public SigningCredentials SigningCredentials { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Func<Task<string>> JtiGenerator =>
        () => Task.FromResult(Guid.NewGuid().ToString());

    /// <summary>
    /// 
    /// </summary>
    public long ToUnixEpochDate()
        => (long)Math.Round((IssuedAt.ToLocalTime() -
                             new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
  }

}