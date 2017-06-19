using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FYP.Twd
{
    
        public interface ITwilioMessageSender
        {
            Task SendMessageAsync(string to, string from, string body);
            
        }
        public class TwilioMessageSender : ITwilioMessageSender
        {
            string account_SID = ConfigurationManager.AppSettings["TwilioAccountSID"];
            string auth_Token = ConfigurationManager.AppSettings["TwilioAuthToken"];
            
            public TwilioMessageSender()
            {

                TwilioClient.Init(account_SID, auth_Token);
                
            }

        public async Task SendMessageAsync(string to, string from, string body)
            {
            try
            {
                await MessageResource.CreateAsync(new PhoneNumber(to),
                                                  from: new PhoneNumber(from),
                                                  body: body);
            }
            catch(Exception  ex)
            {
                //if ((uint)ce.ErrorCode == 0045464) { }

                //RedirectToAction("VerifyPhoneNumber","Manage", new {to });

               
            }


            }
    }

    }