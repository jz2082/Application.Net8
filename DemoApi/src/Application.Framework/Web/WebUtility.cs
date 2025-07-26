using System.Security.Claims;

namespace Application.Framework;

public static class WebUtility
{
    public static string UrlEncode(string value)
    {
        return System.Net.WebUtility.UrlEncode(value);
    }

    public static bool IsClaimExist(ClaimsPrincipal claimsPrincipal, string claimType, string claimValue)
    {
        return (claimsPrincipal.FindFirst(x => x.Type == claimType && x.Value == claimValue) != null);
    }

    public static void AddCustomClaim(ClaimsPrincipal claimsPrincipal, string claimType, string claimValue)
    {
        var claimsIdentity = new ClaimsIdentity();
        if (!string.IsNullOrEmpty(claimValue) && !WebUtility.IsClaimExist(claimsPrincipal, claimType, claimValue))
        {
            claimsIdentity.AddClaim(new Claim(claimType, claimValue));
            if (claimsIdentity.Claims.Count() > 0) claimsPrincipal.AddIdentity(claimsIdentity);
        }
    }

    public static AppUserInfo GetAppUserInfo(ClaimsPrincipal user)
    {
        var userInfo = new AppUserInfo();
        if (user != null)
        {
            if (user.Identity != null)
            {
                userInfo.UserPrincipalName = user.Identity.Name;
                userInfo.IsAuthenticated = user.Identity.IsAuthenticated;
            }
            else
            {
                userInfo.UserPrincipalName = user.FindFirst(ClaimTypes.Name)?.Value;
                userInfo.IsAuthenticated = false;
            }
            string userName = userInfo.UserPrincipalName;
            if (string.IsNullOrEmpty(userName))
            {
                userName = user.FindFirst("preferred_username")?.Value;
                if (!string.IsNullOrEmpty(userName)) userInfo.UserPrincipalName = userName;
            }
            else
            {
                if (userName.Contains("#")) userName = userName.Split("#")[1];
                if (userName.Contains("@")) userName = userName.Split("@")[0];
            }
            userInfo.UserName = userName;
            userInfo.UserId = user.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            if (string.IsNullOrEmpty(userInfo.UserId)) userInfo.UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            userInfo.FirstName = user.FindFirst(ClaimTypes.GivenName)?.Value;
            userInfo.LastName = user.FindFirst(ClaimTypes.Surname)?.Value;
            userInfo.DisplayName = user.FindFirst("name")?.Value;
            userInfo.Birthday = user.FindFirst(ClaimTypes.DateOfBirth)?.Value;
            userInfo.Email = user.FindFirst(ClaimTypes.Email)?.Value;
            userInfo.Phone = user.FindFirst(ClaimTypes.MobilePhone)?.Value;
            userInfo.AlternatePhone = user.FindFirst(ClaimTypes.HomePhone)?.Value;
            userInfo.StreetAddress = user.FindFirst(ClaimTypes.StreetAddress)?.Value;
            userInfo.City = user.FindFirst(ClaimTypes.Locality)?.Value;
            userInfo.Province = user.FindFirst(ClaimTypes.StateOrProvince)?.Value;
            userInfo.Country = user.FindFirst(ClaimTypes.Country)?.Value;
            userInfo.PostalCode = user.FindFirst(ClaimTypes.PostalCode)?.Value;
            userInfo.ClaimList = user.Claims
                .Select(c => new AppKeyValue { Key = c.Type, Value = c.Value })
                .ToList();
            userInfo.GroupList = user.Claims
                .Where(c => c.Type == "groups")
                .Select(c => c.Value)
                .ToList();
            userInfo.RoleList = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }
        return (userInfo);
    }
}