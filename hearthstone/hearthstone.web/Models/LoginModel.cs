using hearthstone.web.App_Code;
using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static hearthstone.web.App_Code.Constants;

namespace hearthstone.web.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [EmailAddress(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = Constants.Validation.EMAIL)]
        [Display(Name = Labels.USERNAME, ResourceType = typeof(DisplayNames))]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = Constants.Validation.REQUIRED)]
        [StringLength(50, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Labels.PASSWORD, ResourceType = typeof(DisplayNames))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}