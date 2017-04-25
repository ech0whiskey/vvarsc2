using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Modules
{
    public class ListFeedModulesForShell_QRM
    {
        public string FeedPID;
        public string FeedInstanceID;
        public string Title;
        public string Language;
        public int ShellID;
        public int Status;
        public bool ShowLikes;
        public bool ShowComments;
        public int ExpirationDays;
        public bool IsAutoApproval;
        public bool AllowHTML;
        public DateTime CreatedOn;
    }
}
