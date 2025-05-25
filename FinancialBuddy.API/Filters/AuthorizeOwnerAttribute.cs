using Microsoft.AspNetCore.Mvc;

namespace FinancialBuddy.API.Filters
{
    public class AuthorizeOwnerAttribute : TypeFilterAttribute
    {
        public AuthorizeOwnerAttribute() : base(typeof(AuthorizeOwnerFilter))
        {
        }
    }
}
