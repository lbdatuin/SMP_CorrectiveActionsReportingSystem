using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Enums
{
    public enum CARStatus
    {
        OPEN,
        NOTIFIED_HEAD,
        SUBMITTED,
        REVIEWED,
        PROCEED,
        UPLOAD_EVIDENCE_OF_FIRST_FOLLOW_UP,
        DONE_FIRST_FOLLOW_UP,
        UPLOAD_EVIDENCE_OF_SECOND_FOLLOW_UP,
        DONE_SECOND_FOLLOW_UP,
        UPLOAD_EVIDENCE_OF_THIRD_FOLLOW_UP,
        DONE_THIRD_FOLLOW_UP,
        CLOSE_EFFECTIVE,
        CLOSE_INEFFECTIVE,
        NOTED
    }
}
