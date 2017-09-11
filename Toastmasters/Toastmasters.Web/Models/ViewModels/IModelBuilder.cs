using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models.ViewModels
{
    public interface IModelBuilder<TViewModel, TEntity>
    {
        TViewModel View(TEntity entity);
        TViewModel Rebuild(TViewModel model);
        TEntity Create(TViewModel actionModel, out String ChangeLog);
        Boolean Update(TViewModel actionModel, TEntity entityEntry, out List<String> ChangeLogs);
    }
}
