using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hearthstone.web.App_Code
{
    public class Constants
    {
        public class Validation
        {
            public const string REQUIRED = "REQUIRED";
            public const string EMAIL = "EMAIL";
            public const string MAX_LENGTH = "MAX_LENGTH";
            public const string PASSWORD_EQUAL = "PASSWORD_EQUAL";
        }

        public class Labels
        {
            public const string USERNAME = "USERNAME";
            public const string PASSWORD= "PASSWORD";
            public const string CONFIRMATION = "CONFIRMATION";
            public const string FIRSTNAME = "FIRSTNAME";
            public const string LASTNAME = "LASTNAME";
            public const string GAMERTAG = "GAMERTAG";

        }
    }
}