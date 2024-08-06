using Intl.Realty.Firm.Models.Models.ViewModel.UserVM;
using Intl.Realty.Firm.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Intl.Realty.Firm.Models.Models.ViewModel.AccountVM;

namespace Intl.Realty.Firm.Utility.Mapper
{
    public static class UserMapper
    {
        public static IEnumerable<DeleteUserViewModel> ToDeleteUserIEnumViewModel(this IEnumerable<User> modelIEnum)
        {
            IEnumerable<DeleteUserViewModel> viewModelIEnum = new List<DeleteUserViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new DeleteUserViewModel()
                {
                    Id = x.Id,
                });
            }
            return viewModelIEnum;
        }
        public static EditUserViewModel ToEditUserViewModel(this User model)
        {
            return new EditUserViewModel
            {
                Id = model.Id,

                FirstName = model.FirstName,
                MiddleName = model.MiddleName ?? "",
                LastName = model.LastName ?? "",
                Suffix = model.Suffix ?? "",
                BirthDate = model.BirthDate,
                ContactNo = model.ContactNo,
                DepartmentId = model.DepartmentId,
                Department = model.Department,
                RoleId = model.RoleId,
                Role = model.Role,
                Email = model.Email,
                Password = model.Password,
                EmploymentStatus = model.EmploymentStatus,
                EmploymentDate = model.EmploymentDate,
                ProfilePictureId = model.ProfilePictureId,

                IsActive = model.IsActive,
                UpdatedBy = model.UpdatedBy,
                UpdatedAt = model.UpdatedAt
            };
        }
        public static IEnumerable<UserViewModel> ToUserIEnumViewModel(this IEnumerable<User> modelIEnum)
        {
            IEnumerable<UserViewModel> viewModelIEnum = new List<UserViewModel>();
            if (modelIEnum != null)
            {
                viewModelIEnum = modelIEnum.Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName ?? "",
                    LastName = x.LastName ?? "",
                    Suffix = x.Suffix ?? "",
                    BirthDate = x.BirthDate,
                    ContactNo = x.ContactNo,
                    DepartmentId = x.DepartmentId,
                    Department = x.Department,
                    RoleId = x.RoleId,
                    Role = x.Role,
                    Email = x.Email,
                    Password = x.Password,
                    EmploymentStatus = x.EmploymentStatus,
                    EmploymentDate = x.EmploymentDate,
                    ProfilePictureId = x.ProfilePictureId,

                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy,
                });
            }
            return viewModelIEnum!;
        }
        public static User ToUserModel(this CreateUserViewModel viewModel)
        {
            var model = new User();

            model.FirstName = viewModel.FirstName;
            model.MiddleName = viewModel.MiddleName ?? "";
            model.LastName = viewModel.LastName ?? "";
            model.Suffix = viewModel.Suffix ?? "";
            model.BirthDate = viewModel.BirthDate;
            model.ContactNo = viewModel.ContactNo;
            model.DepartmentId = viewModel.DepartmentId;
            model.Department = viewModel.Department;
            model.RoleId = viewModel.RoleId;
            model.Role = viewModel.Role;
            model.Email = viewModel.Email;
            model.Password = viewModel.Password;
            model.EmploymentStatus = viewModel.EmploymentStatus;
            model.EmploymentDate = viewModel.EmploymentDate;
            model.ProfilePictureId = viewModel.ProfilePictureId;

            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            return model;
        }
        public static User ToUserModel(this EditUserViewModel viewModel)
        {
            return new User
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                MiddleName = viewModel.MiddleName ?? "",
                LastName = viewModel.LastName ?? "",
                Suffix = viewModel.Suffix ?? "",
                BirthDate = viewModel.BirthDate,
                ContactNo = viewModel.ContactNo,
                DepartmentId = viewModel.DepartmentId,
                Department = viewModel.Department,
                RoleId = viewModel.RoleId,
                Role = viewModel.Role,
                Email = viewModel.Email,
                Password = viewModel.Password,
                EmploymentStatus = viewModel.EmploymentStatus,
                EmploymentDate = viewModel.EmploymentDate,
                ProfilePictureId = viewModel.ProfilePictureId,
                IsActive = viewModel.IsActive,
                UpdatedBy = viewModel.UpdatedBy,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
        public static User ToUserModel(this RegisterViewModel viewModel)
        {
            var model = new User();

            model.FirstName = viewModel.FirstName;
            model.MiddleName = viewModel.MiddleName ?? "";
            model.LastName = viewModel.LastName;
            model.Suffix = viewModel.Suffix ?? "";
            model.BirthDate = viewModel.BirthDate;
            model.ContactNo = viewModel.ContactNumber;
            model.DepartmentId = viewModel.DepartmentId;
            model.RoleId = viewModel.RoleId;
            model.EmploymentStatusId = viewModel.EmploymentStatusId;
            model.Email = viewModel.EmailAddress;
            model.Password = viewModel.CreatePassword;
            model.EmploymentStatus = viewModel.EmploymentStatus;
            model.EmploymentDate = viewModel.DateOfEmployment;
            model.ProfilePictureId = viewModel.ProfilePictureId;

            model.IsActive = viewModel.IsActive;
            model.CreatedAt = viewModel.CreatedAt;
            model.CreatedBy = viewModel.CreatedBy;
            model.UpdatedBy = viewModel.UpdatedBy;
            model.UpdatedAt = viewModel.UpdatedAt;
            return model;
        }
    }
}

