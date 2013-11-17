using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anew.AnewAPIAdapter;
using System.ComponentModel.DataAnnotations;

namespace AnewWeb.Models
{
    public class SessionKey
    {
        [Required(ErrorMessage="Please Enter User Name")]
        [Display(Name = "User Name")]
        public string UserId { get; set; }

        [Required(ErrorMessage="Please Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        public string getSessionKey()
        {
            FlightService fs = new FlightService();
            string sessionKey = string.Empty;
            try
            {
                sessionKey = fs.getSessionKey(UserId, Password);
            }
            catch (Exception)
            {
                return sessionKey;
            }
            return sessionKey;
        }
    }
}