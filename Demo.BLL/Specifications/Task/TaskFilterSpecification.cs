using Demo.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.BLL.Specifications.Task
{
    public class TaskFilterSpecification : BaseSpecifications<TaskEntity>
    {
        public TaskFilterSpecification(TaskSpecificationParams taskSpec)
            : base(t =>
                (string.IsNullOrEmpty(taskSpec.Search) || t.Title!.ToLower().Contains(taskSpec.Search)) &&
                (!taskSpec.Status.HasValue || (int)t.Status == (int)taskSpec.Status.Value))
        {
            ApplyPaging(taskSpec.PageSize * (taskSpec.PageIndex - 1), taskSpec.PageSize);

            // Sorting logic based on Sort parameter
            if (!string.IsNullOrEmpty(taskSpec.Sort))
            {
                switch (taskSpec.Sort)
                {
                    case "dueDateAsc":
                        AddOrderBy(t => t.DueDate);
                        break;
                    case "dueDateDesc":
                        AddOrderByDescending(t => t.DueDate);
                        break;
                    case "createdAtAsc":
                        AddOrderBy(t => t.CreatedAt);
                        break;
                    case "createdAtDesc":
                        AddOrderByDescending(t => t.CreatedAt);
                        break;
                    default:
                        AddOrderBy(t => t.Title);
                        break;
                }
            }
            else
            {
                AddOrderBy(t => t.Title);
            }
        }
        public TaskFilterSpecification(int id) : base(p => p.Id == id)
        {
        }
    }

}
