using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoneBookHumanGroupDL.InterfacesofRepos;
using PhoneBookHumanGroupEL.Entities;
using PhoneBookHumanGroupEL.ViewModels;

namespace PhoneBookHumanGroupPL.Controllers
{
    public class DenemeController : Controller
    {
        //NOT: BL katmanındaki managerlar olmadan da bir controller içinde işlem yapalım

        private readonly IPhoneGroupRepo _phoneGroupRepo;
        private readonly IMemberPhoneRepo _memberPhoneRepo;
        private readonly IMapper _mapper;

        public DenemeController(IPhoneGroupRepo phoneGroupRepo, IMemberPhoneRepo memberPhoneRepo, IMapper mapper)
        {
            _phoneGroupRepo = phoneGroupRepo;
            _memberPhoneRepo = memberPhoneRepo;
            _mapper = mapper;
        }

        public IActionResult AllPhoneGroups()
        {
            try
            {
                //1. yol 
                //var data = _phoneGroupRepo.GetAll().ToList();
                //return View("AllPhoneGroupView1", data);

                //2.yol Bu işlem best practice
                var data = _phoneGroupRepo.GetAll();

                List<PhoneGroupVM> datalist = new List<PhoneGroupVM>();

                var model = _mapper.Map<IQueryable<PhoneGroup>, List<PhoneGroupVM>>(data);
                foreach (var item  in model)
                {
                    item.ContactCount=_memberPhoneRepo.GetAll(x=> x.PhoneGroupId==item.Id).ToList().Count;
                }
                return View("AllPhoneGroupView2", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
