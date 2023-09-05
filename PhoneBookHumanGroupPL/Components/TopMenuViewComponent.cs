using Microsoft.AspNetCore.Mvc;
using PhoneBookHumanGroupBL.InterfacesOfManagers;
using PhoneBookHumanGroupEL.ViewModels;

namespace PhoneBookHumanGroupPL.Components
{
    public class TopMenuViewComponent: ViewComponent
    {
        private readonly IMemberManager _memberManager;

        public TopMenuViewComponent(IMemberManager memberManager)
        {
            _memberManager = memberManager;
        }

        public IViewComponentResult Invoke()
        {
            try
            {
                var loggedInUserEmail = HttpContext.User.Identity.Name;
                if (!string.IsNullOrEmpty(loggedInUserEmail))
                {
                    var loggedInUser = _memberManager.GetByCondition(x => x.Email.ToLower() == loggedInUserEmail.ToLower()).Data;
                     return View("TopMenuTheme", loggedInUser);

                }

                return View("TopMenuTheme", new MemberVM());
            }
            catch (Exception ex)
            {
                return View("TopMenuTheme", new MemberVM());
            }
        }
    }
}
