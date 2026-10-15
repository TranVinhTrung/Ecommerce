using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class CultureController : Controller
    {
        //Controller này lưu lựa chọn ngôn ngữ vào cookie một năm rồi quay lại trang hiện tại. Middleware Localization sẽ đọc cookie đó ở các request tiếp theo.

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });

            return LocalRedirect(returnUrl);
        }
    }
}
