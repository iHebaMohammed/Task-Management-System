using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public enum TaskStatusEnum
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "InProgress")]
        InProgress,
        [EnumMember(Value = "Completed")]
        Completed
    }
}
