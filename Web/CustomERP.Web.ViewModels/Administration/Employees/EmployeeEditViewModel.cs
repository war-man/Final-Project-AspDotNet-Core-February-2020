﻿namespace CustomERP.Web.ViewModels.Administration.Employees
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CustomERP.Data.Models;
    using CustomERP.Services.Mapping;
    using CustomERP.Web.ViewModels.Shared;

    public class EmployeeEditViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Full Name")]
        [RegularExpression(
            "^([A-Z][a-z]+\\s[A-Z][a-z]+\\s[A-Z][a-z]+|[А-Я][а-я]+\\s[А-Я][а-я]+\\s[А-Я][а-я]+)",
            ErrorMessage = "The \"Full Name\" field must be in the format \"Firstname Secondname Lastname\", each beginning with a capital letter, separated by one space.")]
        public string FullName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The \"Position\" field must be between '3' and '20' characters long", MinimumLength = 3)]
        [MaxLength(20)]
        public string Position { get; set; }

        [Display(Name = "Team")]
        public int? TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; }

        [EmailAddress]
        [Display(Name = "Address")]
        public string Email { get; set; }

        public IEnumerable<TeamDropDownViewModel> Teams { get; set; }

        [Display(Name = "Address")]
        public int? AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }

        [Display(Name = "Section")]
        public int? SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }

        public IEnumerable<SectionDropDownViewModel> Sections { get; set; }

        [Display(Name = "Company")]
        public string CompanyId { get; set; }

        public IEnumerable<CompanyDropDownViewModel> Companies { get; set; }

        [Display(Name = "Manager")]
        public string ManagerId { get; set; }

        public IEnumerable<ApplicationUserDropDownViewModel> ApplicationUsers { get; set; }
    }
}
