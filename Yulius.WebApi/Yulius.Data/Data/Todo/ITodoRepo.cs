using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yulius.Data.FilterParams;
using Yulius.Data.Helpers;
using Yulius.Data.Models;

namespace Yulius.Data.Data.Todo
{
    public interface ITodoRepo
    {
        Task<Result> TodoSave(UserParams userParams, TodoItem todoItem);

        Task<Result> TodoList(UserParams userParams, PageParamsForPost pageParams, TodoFilterParams filterParams);

    }
}
