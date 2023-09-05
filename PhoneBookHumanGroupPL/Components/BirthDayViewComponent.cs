using Microsoft.AspNetCore.Mvc;
using PhoneBookHumanGroupBL.InterfacesOfManagers;
using PhoneBookHumanGroupEL.ViewModels;

namespace PhoneBookHumanGroupPL.Components
{
    public class BirthDayViewComponent : ViewComponent
    {
        private readonly IMemberManager _memberManager;

        public BirthDayViewComponent(IMemberManager memberManager)
        {
            _memberManager = memberManager;
        }

        public IViewComponentResult Invoke()
        {
            var loggedInUserEmail = HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(loggedInUserEmail))
            {
                var loggedInUser = _memberManager.GetByCondition(x => x.Email.ToLower() == loggedInUserEmail.ToLower()).Data;
                return View(loggedInUser);

            }
            return View(new MemberVM());
        }
    }
}
