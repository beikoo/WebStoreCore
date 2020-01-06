using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interface
{
    public interface IAuditInfo
    {
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
