﻿namespace CustomERP.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CustomERP.Services.Data;
    using CustomERP.Web.ViewModels.Administration.Accounts;
    using CustomERP.Web.ViewModels.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class AccountsController : AdministrationController
    {
        private readonly IApplicationUserService userService;
        private readonly ICompaniesService companiesService;
        private readonly ISectionsService sectionsService;
        private readonly IShiftsService shiftService;

        public AccountsController(
            IApplicationUserService userService,
            ICompaniesService companiesService,
            ISectionsService sectionsService,
            IShiftsService shiftService)
        {
            this.userService = userService;
            this.companiesService = companiesService;
            this.sectionsService = sectionsService;
            this.shiftService = shiftService;
        }

        // GET: /Accounts/Index
        [HttpGet]
        public IActionResult Index()
        {
            var users = this.userService.GetAll<IndexEmployeeViewModel>();

            var viewModel = new IndexViewModel
            {
                Users = users,
            };

            return this.View(viewModel);
        }

        // GET: /Accounts/Details/id
        public IActionResult Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.NotFound();
            }

            var viewModel = this.userService.GetById<EmployeeDetailsViewModel>(id);

            return this.View(viewModel);
        }

        // GET: /Accounts/RegisterEmployee
        public IActionResult RegisterEmployee()
        {
            var company =
                this.companiesService
                .GetAll<CompanyDropDownViewModel>()
                .Prepend(new CompanyDropDownViewModel { Id = string.Empty, Name = string.Empty });

            var sections =
                this.sectionsService
                .GetAll<SectionDropDownViewModel>()
                .Prepend(new SectionDropDownViewModel() { Id = null, Name = string.Empty });

            var shifts =
                this.shiftService
                .GetAll<ShiftDropDownViewModel>()
                .Prepend(new ShiftDropDownViewModel() { Id = null, Name = string.Empty });

            var users =
                this.userService
                .GetAll<ApplicationUserDropDownViewModel>()
                .Prepend(new ApplicationUserDropDownViewModel { Id = string.Empty, FullName = string.Empty });

            var viewModel = new EmployeeRegisterViewModel
            {
                Companies = company,
                Sections = sections,
                Shifts = shifts,
                ApplicationUsers = users,
            };

            return this.View(viewModel);
        }

        // POST:  /Accounts/RegisterEmployee
        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(EmployeeRegisterViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            inputModel.CreatedFrom = this.User.Identities.FirstOrDefault()?.Name;

            var existingЕmployeeId = this.userService.GetIdByFullName(inputModel.FullName);

            if (existingЕmployeeId == null)
            {
                var userId = await this.userService.RegisterAsync(inputModel);

                return this.RedirectToAction(nameof(this.Details), new { id = userId});
            }

            return this.RedirectToAction(nameof(this.Details), new { id = existingЕmployeeId});
        }
    }
}